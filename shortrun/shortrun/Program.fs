open System.Collections.Concurrent
open System.Diagnostics
open System.IO
open System.Text;
open System.Threading.Tasks
open System.Xml

type testRun = { Serial: bool; AssemblyPath: string; }
type testResult = { Run : uint32; Errors: uint32; Failures : uint32; Text : string; Test : testRun; }
type testConfig = { NUnitConsolePath : string; TimeoutMillis : int32; Verbose: bool; TestFilePath: string}

let results = new ConcurrentBag<testResult>()

let getTestRunsFromXml (nunit : XmlDocument) = 
    seq { for a in nunit.SelectNodes("//NUnitProject/Config/assembly") do
            let serialNode = a.Attributes.GetNamedItem("serial")            
            let isSerial = not (serialNode = null) && not (serialNode.Value = null) && serialNode.Value.ToUpper() = "TRUE"   // todo create some sort of istrue func and use this here and for verbose             
            let pathNode = a.Attributes.GetNamedItem("path")
            let path = if pathNode = null then "" else pathNode.Value
            yield { Serial = isSerial; AssemblyPath = path; }
        }

let getTestRunsFromFile (nunitpath : string) = 
    if File.Exists nunitpath then
        let x = new XmlDocument()
        x.Load(nunitpath)
        getTestRunsFromXml x
    else
        Seq.empty<testRun>

let createTestProcess (test : testRun) (config : testConfig) =        
    //nunit-console timeout is per-test, which isn't what we want, but might still cause a test run to abandoned, so is included
    let args = sprintf "%s /timeout=%d" test.AssemblyPath config.TimeoutMillis
    let pi = new ProcessStartInfo(config.NUnitConsolePath, args)
    pi.WorkingDirectory <- (new System.IO.FileInfo(config.TestFilePath)).DirectoryName
    pi.RedirectStandardOutput <- true
    pi.CreateNoWindow <- true
    pi.UseShellExecute <- false
    
    let p = new System.Diagnostics.Process()
    p.StartInfo <- pi
    p

let addTestResult (config : testConfig) (test : testRun) (output : string) = 
    let exp = new RegularExpressions.Regex("Tests run: ([0-9]+), Errors: ([0-9]+), Failures: ([0-9]+)")
    let expMatch = exp.Match output

    if expMatch.Success then
        let run = (System.Convert.ToUInt32 (expMatch.Groups.[1].Captures.[0].Value))
        let errors = (System.Convert.ToUInt32 (expMatch.Groups.[2].Captures.[0].Value))
        let failures = (System.Convert.ToUInt32 (expMatch.Groups.[3].Captures.[0].Value))

        let result = { Run = run; Errors = errors; Failures = failures; Text = output; Test = test; }

        results.Add(result)

    elif config.Verbose then
        printfn "No match for regex using output '%s'" output

let getOutput (config : testConfig) (p : Process) =
    let o = new StringBuilder()
    let s = Stopwatch.StartNew()
    
    while not(p.WaitForExit 10) && int(s.ElapsedMilliseconds) < config.TimeoutMillis do
        ignore(o.Append(p.StandardOutput.ReadToEnd()))

    ignore(o.Append(p.StandardOutput.ReadToEnd()))

    if not p.HasExited then p.Kill() 

    o.ToString()

let runTest (config : testConfig) (test : testRun)  =      
    async {  
        let p = createTestProcess test config

        if (File.Exists p.StartInfo.FileName) && p.Start() then            
            addTestResult config test (getOutput config p)

        ()    
    }

let runTests (tests : testRun list) (config : testConfig) = 
    List.map (runTest config) tests |> Async.Parallel |> Async.RunSynchronously

let getArgValue (arg : string) = 
    let sep = '='
    let indexOfSep = arg.IndexOf(sep)
    if indexOfSep >= 0 then
        arg.Substring(indexOfSep + 1, arg.Length - indexOfSep - 1)
    else
        arg

let getArgByName (argName : string) (args : string array) = 
    let casedArgName = argName.ToUpper()
    let mutable res = ""
    for arg in args do
        if arg.ToUpper().StartsWith(casedArgName) then
            res <- getArgValue arg
    res

let getArgByNameWithDefault argName args defaultValue = 
    let supplied = getArgByName argName args
    if System.String.IsNullOrEmpty supplied then
        defaultValue
    else
        supplied

let getNUnitPath args = 
    getArgByNameWithDefault "r" args "c:\\program files (x86)\\nunit 2.5.9\\bin\\net-2.0\\nunit-console.exe"

let getTimeout args = 
    let timeout = getArgByNameWithDefault "t" args "60000"
    int32 timeout

let getNUnitFilePath args = 
    getArgByName "f" args

let getVerbose args = 
    (getArgByName "v" args).ToUpper() = "TRUE"

let printUsage () = 
    printfn "shortrun.exe f={path to .nunit file} t={optional timeout in milliseconds} r={optional path to nunit-console.exe} v={optional verbose logging, true or false}\r\n"

let getConfig args = 
    { NUnitConsolePath = getNUnitPath args; TimeoutMillis = getTimeout args; Verbose = getVerbose args; TestFilePath = getNUnitFilePath args }

let printcol col fmt = 
    Printf.kprintf
        (fun s -> 
            let orig = System.Console.ForegroundColor
            System.Console.ForegroundColor <- col
            System.Console.WriteLine s
            System.Console.ForegroundColor <- orig)
        fmt

let getColour result = 
    let notPassed = result.Failures + result.Errors
    if notPassed > 0u then
        System.ConsoleColor.Red
    elif result.Run = 0u then
        System.ConsoleColor.Yellow
    else
        System.ConsoleColor.White

let printVerboseResult result = 
            System.Console.WriteLine()
            printcol System.ConsoleColor.Blue "%s" result.Test.AssemblyPath
            System.Console.WriteLine result.Text 

let printResult verbose result =
    if verbose then printVerboseResult result
    else printcol (getColour result) "%s\r\nRun: %d\tErrors: %d\tFailures: %d\r\n" result.Test.AssemblyPath result.Run result.Errors result.Failures

let printSummary results = 
    let mutable run = 0u
    let mutable errs = 0u
    let mutable failures = 0u
    for result in results do
        run <- run + result.Run
        errs <- errs + result.Errors
        failures <- failures + result.Failures

//    print "Results across all tests."
    let allResults = { Run = run; Errors = errs; Failures = failures; Text = ""; Test = { Serial = false; AssemblyPath = "";}}
    printf "Result of all test runs:"
    printResult false allResults

let printResults config = 
    Seq.iter (printResult config.Verbose) results
    printSummary results



 
let createParallelSets (tests : seq<testRun>) = 
    seq {
    //yield all the serial tests as single item lists
    for t in (Seq.filter  (function (x : testRun) -> x.Serial) tests) -> [t]
    
    //and then yield all the parallel tests in one list
    yield [ 
        for t in (Seq.filter (function x -> not x.Serial) tests) -> t
        ]
    }

[<EntryPoint>]
let main args = 
    printUsage()


    let tests = getTestRunsFromFile (getNUnitFilePath args)    
    let config = getConfig args

    printfn "Using nunit path: %s" config.NUnitConsolePath
    printfn "Using timeout millis: %d" config.TimeoutMillis
    printfn "Using verbose: %b" config.Verbose

    if System.IO.File.Exists config.NUnitConsolePath then
        
        printcol System.ConsoleColor.Blue "Running tests in %d assemblies." (Seq.length tests)

        let s = Stopwatch.StartNew()

        for testSet in createParallelSets tests do
            ignore (runTests testSet config)

        s.Stop()
        printcol System.ConsoleColor.Blue "Completed in %d milliseconds" s.ElapsedMilliseconds
        printResults config
    else
        printcol System.ConsoleColor.Red "Path to nunit console '%s' does not exist. No tests run." config.NUnitConsolePath

    0

