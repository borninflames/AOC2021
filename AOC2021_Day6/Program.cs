Console.WriteLine("Hello, Advent of Code 2021!");

//var input = "3,4,3,1,2";
var input = "1,3,4,1,5,2,1,1,1,1,5,1,5,1,1,1,1,3,1,1,1,1,1,1,1,2,1,5,1,1,1,1,1,4,4,1,1,4,1,1,2,3,1,5,1,4,1,2,4,1,1,1,1,1,1,1,1,2,5,3,3,5,1,1,1,1,4,1,1,3,1,1,1,2,3,4,1,1,5,1,1,1,1,1,2,1,3,1,3,1,2,5,1,1,1,1,5,1,5,5,1,1,1,1,3,4,4,4,1,5,1,1,4,4,1,1,1,1,3,1,1,1,1,1,1,3,2,1,4,1,1,4,1,5,5,1,2,2,1,5,4,2,1,1,5,1,5,1,3,1,1,1,1,1,4,1,2,1,1,5,1,1,4,1,4,5,3,5,5,1,2,1,1,1,1,1,3,5,1,2,1,2,1,3,1,1,1,1,1,4,5,4,1,3,3,1,1,1,1,1,1,1,1,1,5,1,1,1,5,1,1,4,1,5,2,4,1,1,1,2,1,1,4,4,1,2,1,1,1,1,5,3,1,1,1,1,4,1,4,1,1,1,1,1,1,3,1,1,2,1,1,1,1,1,2,1,1,1,1,1,1,1,2,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,1,1,2,5,1,2,1,1,1,1,1,1,1,1,1";
// [<3,2>, <4,1>, <1,1>, <2,1>]
// [<2,2>, <3,1>, <0,1>, <1,1>]

//<days, fish count>
var lifeCycle = new Dictionary<int, long>();

foreach (var item in input.Split(','))
{
    var days = Convert.ToInt32(item);
    if (lifeCycle.ContainsKey(days))
    {
        lifeCycle[days]++;
    }
    else
    {
        lifeCycle.Add(days, 1);
    }
}

for (int day = 1; day <= 256; day++)
{
    var newDay = new Dictionary<int, long>();
    foreach (var item in lifeCycle)
    {
        if (item.Key != 0)
        {
            if (newDay.ContainsKey(item.Key - 1))
            {
                newDay[item.Key - 1] += item.Value;
            }
            else
            {
                newDay[item.Key - 1] = item.Value;
            }                
        }
        else
        {
            if (newDay.ContainsKey(6))
            {
                newDay[6] += item.Value;
            }
            else
            {
                newDay[6] = item.Value;
            }

            if (newDay.ContainsKey(8))
            {
                newDay[8] += item.Value;
            }
            else
            {
                newDay[8] = item.Value;
            }
        }
    }

    lifeCycle = newDay;
}

long sumOfFish = 0l;
foreach (var item in lifeCycle)
{
    sumOfFish += item.Value;
}


Console.WriteLine(sumOfFish);
