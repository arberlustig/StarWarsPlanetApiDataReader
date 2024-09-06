
using System.Numerics;
using System.Reflection.Metadata.Ecma335;


//var time1 = new Time(1, 2);
//var time2  = new Time(2, 3);

//var tuple1 = new Tuple<string,bool>("aaa",false);
//var tuple2 = Tuple.Create(10,true);
//var tuple3 = Tuple.Create(10, true);

//Console.WriteLine(tuple2 == tuple3);
//Console.WriteLine(tuple2.Equals(tuple3));


//Console.ReadKey();

//var addedTime = time1 + time2;
//Console.WriteLine( time1.ToString());
//Console.WriteLine(addedTime.ToString());
//Console.ReadKey();
public struct Time
{
    public int Hour { get; }
    public int Minute { get; }

    public Time(int hour, int minute)
    {
        if (hour < 0 || hour > 23)
        {
            throw new ArgumentOutOfRangeException(
                "Hour is out of range of 0-23");
        }
        if (minute < 0 || minute > 59)
        {
            throw new ArgumentOutOfRangeException(
                "Minute is out of range of 0-59");
        }
        Hour = hour;
        Minute = minute;
    }

    public static Time operator +(Time time1, Time time2)
    {
        int totalMinutes = time1.Minute + time2.Minute;
        int totalHours = time1.Hour + time2.Hour + totalMinutes / 60;
        totalMinutes %= 60;
        totalHours %= 24;

        return new Time(totalHours, totalMinutes);
    }

    public override string ToString() =>
        $"{Hour.ToString("00")}:{Minute.ToString("00")}";

    public override bool Equals(object? obj)
    {
        return obj is Time time &&
               Hour == time.Hour &&
               Minute == time.Minute;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Hour, Minute);
    }

    public static bool operator ==(Time time1, Time time2)
    {
        return time1.Equals(time2);
    }
    public static bool operator !=(Time time1, Time time2)
    {
        return !time1.Equals(time2);
    }



}
