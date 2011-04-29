def sidelensupto(n):
    curr = 1
    sides = []
    while curr <= n:
        sides.append(curr)
        curr = curr + 2
    return sides
    
def nitemsinspiralofside(side):
    if side == 1:
        return 1
    else:
        return 4 * side - 4

def nextcorner(prev, sidelen):
    return prev + sidelen - 1

def getdiags (sidelen, totalofsmallerspirals):
  #  print "getdiags %d %d" % (sidelen, totalofsmallerspirals)
    first = nextcorner(totalofsmallerspirals, sidelen)
    second = nextcorner(first, sidelen)
    third = nextcorner(second, sidelen)
    fourth = nextcorner(third, sidelen)
 #   print "Adding %d %d %d %d for %d" % (first, second, third, fourth, sidelen)
    return first + second + third + fourth

diag = 1
total = 1 #for inner square
for s in sidelensupto(1001):
    if s > 1:
#        print "side %d" % s
        spiral = nitemsinspiralofside(s)
#        print "spiral of side %d has %d, total before is %d" % (s, spiral, total)
        diagcontrib = getdiags(s, total)
        total += spiral
        diag += diagcontrib

print "diag sum is %d" % diag     #669171001