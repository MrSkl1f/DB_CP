# Задача 2
s = str(input())
letters = "aeiouy"

flag = 1 if s[0].lower() in letters else 0
res = True
    
for i in range(1, len(s), 1):
    check = s[i].lower() in letters
    if (check and flag == 0):
        flag = 1
    elif (not check and flag) == 1:
        flag = 0
    else:
        res = False
        break
if res:
    print("YES")
else:
    print("NO")

# Задача 3
line = input().split()
a = int(line[0])
b = int(line[1])
c = int(line[2])
t = int(line[3])

seconds = t % c
generalMinutes = t // c
minutes = generalMinutes % b
hours = generalMinutes // b
while hours >= a:
    hours -= a

print(f'{hours} {minutes} {seconds}')


    



