from mimesis import Person, Address
from mimesis.enums import Gender
from random import randint, randrange, choice
import urllib.request, urllib.parse, urllib.error
import json
import secrets
import string

ALPHABET = string.ascii_letters + string.digits
person = Person("en")
address = Address("en")

def generateLoginAndPassword():
    return ''.join(secrets.choice(ALPHABET) for i in range(5))


def checkData(data):
    if  data['strTeam'] != '' and data['strTeam'] != None and\
        data['strCountry'] != '' and data['strCountry'] != None and data['strCountry'] != 'International' and\
        data['strStadium'] != '' and data['strStadium'] != None:
            return True
    return False

# TeamID, ManagementID, TeamName, HeadCoach, Country, Stadium
def generateTeam():
    f = open("Team.csv", "w", encoding='utf-8')
    MAX_N = 50
    url = 'https://www.thesportsdb.com/api/v1/json/1/all_leagues.php'
    leagues = json.loads(urllib.request.urlopen(url).read().decode())
    
    countOfLeagues = sum([1 for item in leagues['leagues']])
    currTeamID = 1
    currManagementID = 1
    check = 0
    
    for i in range(countOfLeagues):
        # Check if sport is Basketball
        if leagues['leagues'][i]['strSport'] == 'Basketball':
            url = 'https://www.thesportsdb.com/api/v1/json/1/lookup_all_teams.php?id=' + str(leagues['leagues'][i]['idLeague'])
            # Get all teams in current league
            teamsInLeague = json.loads(urllib.request.urlopen(url).read().decode()) 
            if teamsInLeague['teams'] != None:
                countOfTeams = sum([1 for item in teamsInLeague['teams']])
                for j in range(countOfTeams):
                    # Do while count of need teams not max
                    if (currTeamID <= MAX_N):
                        if checkData(teamsInLeague['teams'][j]):
                            team =  str(currTeamID)                         + ',' +\
                                    str(currManagementID)                   + ',' +\
                                    teamsInLeague['teams'][j]['strTeam']    + ',' +\
                                    person.full_name()                      + ',' +\
                                    teamsInLeague['teams'][j]['strCountry'] + ',' +\
                                    teamsInLeague['teams'][j]['strStadium'] + ',' +\
                                    str(randint(10000, 100000)) + '\n'
                            currTeamID          += 1
                            currManagementID    += 1
                            f.write(team)
                    else:
                        check = 1
                        break
        if check:
            break
    f.close()

# id, login, password
def generateUserInfo():
    MAX_N = 100
    f = open("UserInfo.csv", "w", encoding='utf-8')
    currPerm = 0
    for userID in range(1, MAX_N + 1, 1):
        currPerm += 1
        if currPerm > 2:
            currPerm = 1
        info =  str(userID)                 + ',' +\
                generateLoginAndPassword()  + ',' +\
                generateLoginAndPassword()  + ',' +\
                str(currPerm) + '\n'
        f.write(info)
    f.close()

def generateManagement():
    MAX_N = 50
    currID = 1
    f = open("Management.csv", "w", encoding='utf-8')
    for Management in range(1, MAX_N + 1, 1):
        info =  str(Management) + ',' +\
                str(currID)     + ',' +\
                str(currID + 1) + '\n'
        currID += 2
        f.write(info)
    f.close()

def generateStatistics():
    MAX_N = 250
    f = open("Statistics.csv", "w", encoding='utf-8')
    
    for StatisticsID in range(1, MAX_N + 1):
        info =  str(StatisticsID)   + ',' +\
                str(randint(0, 40)) + ',' +\
                str(randint(0, 60)) + '\n'
        f.write(info)
    f.close()

# PlayerID, TeamID, Statistics, PlayerName, position, weight, height, number, age, country, cost
# position = (la, ca, ra, ldef, rdef)
def generatePlayers():
    MAX_N = 250
    TeamID = 1
    countTeam = 0
    StatisticsID = 1
    positions = ['la', 'ca', 'ra', 'ldef', 'rdef']
    position = 0
    f = open("Player.csv", "w", encoding='utf-8')
    for PlayerID in range(1, MAX_N + 1):
        info =  str(PlayerID) + ',' +\
                str(TeamID) + ',' +\
                str(StatisticsID) + ',' +\
                person.full_name() + ',' +\
                positions[position] + ',' +\
                str(person.weight()) + ',' +\
                str(randint(150, 220)) + ',' +\
                str(randint(0, 100)) + ',' +\
                str(person.age()) + ',' +\
                address.country(allow_random=True) + ',' +\
                str(randint(0, 5000)) + '\n'
        f.write(info)
        countTeam += 1
        if countTeam == 5:
            countTeam = 0
            TeamID += 1
        position += 1
        if position == 5:
            position = 0
        StatisticsID += 1
    f.close()
generatePlayers()