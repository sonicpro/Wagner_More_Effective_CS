using System;
using System.Timers;

namespace PatternMatchingDemos
{
    public static class TollCalculations
    {
        public static bool IsWeekDay(DateTime timeOFTool) =>
            timeOFTool.DayOfWeek switch
            {
                DayOfWeek.Saturday => false,
                DayOfWeek.Sunday => false,
                _ => true
            };

        private enum TimeBand
        {
            MorningRush,
            Daytime,
            EveningRush,
            Overnight
        }

        // There is no "range pattern" at the moment, so the next function
        // is written with old-school "if" statements.
        private static TimeBand GetTimeBand(DateTime timeOfToll)
        {
            int hour = timeOfToll.Hour;
            if (hour < 6)
                return TimeBand.Overnight;
            else if (hour < 10)
                return TimeBand.MorningRush;
            else if (hour < 16)
                return TimeBand.Daytime;
            else if (hour < 20)
                return TimeBand.EveningRush;
            else
                return TimeBand.Overnight;
        }
        // Calculate a peak time multiplier:
        // weekend multiplier is 1.0
        // late night / early morning is a discount, 0.75
        // daytime during any weekday is 1.5
        // morning rush inbound is double (2.0)
        // morning rush outbound is 1.0
        // evening rush inbound is 1.0
        // evening rush outbound is double (2.0)

        // The "imperative" way to calculate the tall.
        public static decimal PeakTimePremiumImperative(DateTime timeOfToll, bool inbound)
        {
            var timeBand = GetTimeBand(timeOfToll);
            if (IsWeekDay(timeOfToll))
            {
                if (inbound)
                {
                    if (timeBand == TimeBand.MorningRush)
                    {
                        return 2.00m;
                    }
                    else if (timeBand == TimeBand.Daytime)
                    {
                        return 1.50m;
                    }
                    else if (timeBand == TimeBand.EveningRush)
                    {
                        return 1.00m;
                    }
                    else // if (timeBand == TimeBand.Overnight)
                    {
                        return 0.75m;
                    }
                }
                else
                {
                    if (timeBand == TimeBand.MorningRush)
                    {
                        return 1.00m;
                    }
                    else if (timeBand == TimeBand.Daytime)
                    {
                        return 1.50m;
                    }
                    else if (timeBand == TimeBand.EveningRush)
                    {
                        return 2.00m;
                    }
                    else // if (timeBand == TimeBand.Overnight)
                    {
                        return 0.75m;
                    }
                }
            }
            else // on Weekends
            {
                return 1.00m;
            }
        }

        // Calculate a peak time multiplier:
        // weekend multiplier is 1.0
        // late night / early morning is a discount, 0.75
        // daytime during any weekday is 1.5
        // morning rush inbound is double (2.0)
        // morning rush outbound is 1.0
        // evening rush inbound is 1.0
        // evening rush outbound is double (2.0)

        // Do the same as the above function using pattern matching.
        public static decimal PeekTimePremium(DateTime timeOfToll, bool inbound) =>
            (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
            {
                (true, TimeBand.MorningRush, true) => 2.00m,
                (true, TimeBand.MorningRush, false) => 1.00m,
                (true, TimeBand.Daytime, _) => 1.50m,
                (true, TimeBand.EveningRush, true) => 1.00m,
                (true, TimeBand.EveningRush, false) => 2.00m,
                (true, _, _) => 0.75m,
                (false, _, _) => 1.00m
            };

        public static (DateTime start, DateTime end) GenerateSubscription()
        {
            var start = DateTime.Now.Date;
            // the next line will crash when the method runs 29 Feb 2020.
            //var end = new DateTime(start.Year + 1, start.Month, start.Day);
            var end = start.AddYears(1);
            return (start, end);
        }
    }
}
