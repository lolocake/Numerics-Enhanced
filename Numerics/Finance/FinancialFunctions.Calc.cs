
﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
    public sealed partial class FinancialFunctions
    {
        private static double AccrInt(DateTime issue, DateTime firstInterestData, DateTime settlement, double rate, double par, double frequency, DayCountBasis basis, AccrIntCalcMethod calcMethod)
        {
            DateTime pdc;
            var dc = DayCount.DayCounterFactory(basis);

            var numMonths = DayCount.Freq2Months(frequency);
            var numMonthsNeg = -numMonths;
            var endMonthBond = Common.LastDayOfMonth(firstInterestData.Year, firstInterestData.Month, firstInterestData.Day);
            if (settlement > firstInterestData && calcMethod == AccrIntCalcMethod.FromIssueToSettlement) pdc = DayCount.FindPcdNcd(firstInterestData, settlement, numMonths, basis, endMonthBond).Item1;
            else pdc = dc.ChangeMonth(firstInterestData, numMonthsNeg, endMonthBond);
            var firstDate = issue > pdc ? issue : pdc;

            var days = dc.DaysBetween(firstDate, settlement, NumDenumPosition.Numerator);
            var dayCount2 = dc;
            var dateTime5 = pdc;
            var dateTime6 = firstInterestData;

            var coupDays = dayCount2.CoupDays(dateTime5, dateTime6, frequency);
            var aggFunction = FAccrInt(issue, basis, calcMethod, dc, frequency);
            var tuple1 = DayCount.DatesAggregate1(pdc, issue, numMonthsNeg, basis, aggFunction, days / coupDays, endMonthBond);
            var a = tuple1.Item3;
            return par * rate / frequency * a;
        }

        private static double AccrIntM(DateTime issue, DateTime settlement, double rate, double par, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var days = dc.DaysBetween(issue, settlement, NumDenumPosition.Numerator);
            var daysInYear = dc.DaysInYear(issue, settlement);
            return par * rate * days / daysInYear;
        }

        private static double Disc(DateTime settlement, DateTime maturity, double pr, double redemption, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("investment must be more than 0", pr > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);
            return DiscInternal(settlement, maturity, pr, redemption, basis);
        }

        private static double Duration(DateTime settlement, DateTime maturity, double coupon, double yld, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("coupon must be more than 0", coupon >= 0);
            Common.Ensure("yld must be more than 0", yld >= 0);
            return DurationInternal(settlement, maturity, coupon, yld, (double)frequency, basis, false);
        }

        private static double IntRate(DateTime settlement, DateTime maturity, double investment, double redemption, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("investment must be more than 0", investment > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);
            return IntRateInternal(settlement, maturity, investment, redemption, basis);
        }

        private static double MDuration(DateTime settlement, DateTime maturity, double coupon, double yld, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("coupon must be more than 0", coupon >= 0);
            Common.Ensure("yld must be more than 0", yld >= 0);
            return DurationInternal(settlement, maturity, coupon, yld, (double)frequency, basis, true);
        }

        private static double Price(DateTime settlement, DateTime maturity, double rate, double yld, double redemption, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("rate must be more than 0", rate > 0);
            Common.Ensure("yld must be more than 0", yld > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);
            return PriceInternal(settlement, maturity, rate, yld, redemption, (double)frequency, basis);
        }

        private static double PriceDisc(DateTime settlement, DateTime maturity, double discount, double redemption, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("investment must be more than 0", discount > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);
            return PriceDiscInternal(settlement, maturity, discount, redemption, basis);
        }

        private static double Received(DateTime settlement, DateTime maturity, double investment, double discount, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var dim = dc.DaysBetween(settlement, maturity, NumDenumPosition.Numerator);
            var b = dc.DaysInYear(settlement, maturity);
            var discountFactor = discount * dim / b;
            Common.Ensure("discount * dim / b must be different from 1", discountFactor < 1);
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("investment must be more than 0", investment > 0);
            Common.Ensure("redemption must be more than 0", discount > 0);
            return ReceivedInternal(settlement, maturity, investment, discount, basis);
        }

        private static double Yield(DateTime settlement, DateTime maturity, double rate, double pr, double redemption, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("rate must be more than 0", rate > 0);
            Common.Ensure("pr must be more than 0", pr > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);

            var frq = (double)frequency;
            var priceYieldFactors = GetPriceYieldFactors(settlement, maturity, frq, basis);

            var n = priceYieldFactors.Item1;
            var e = priceYieldFactors.Item4;
            var dsr = priceYieldFactors.Item5;
            var a = priceYieldFactors.Item3;
            if (n > 1) return Common.FindRoot(FYield(settlement, maturity, rate, pr, redemption, frq, basis), 0.05);
            const double Par = 100;
            var firstNum = redemption / 100 + rate / frq - (Par / 100 + a / e * rate / frq);
            var firstDen = Par / 100 + a / e * rate / frq;
            return firstNum / firstDen * frq * e / dsr;
        }

        private static double YieldDisc(DateTime settlement, DateTime maturity, double pr, double redemption, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("investment must be more than 0", pr > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);
            return YieldDiscInternal(settlement, maturity, pr, redemption, basis);
        }

        private static double YieldMat(DateTime settlement, DateTime maturity, DateTime issue, double rate, double pr, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("maturity must be after issue", (maturity > issue));
            Common.Ensure("settlement must be after issue", (settlement > issue));
            Common.Ensure("rate must be more than 0", rate > 0);
            Common.Ensure("price must be more than 0", pr > 0);
            //return yieldMat(settlement, maturity, issue, rate, pr, basis);
            var matFactors = GetMatFactors(settlement, maturity, issue, basis);
            var dsm = matFactors.Item4;
            var dim = matFactors.Item2;
            var b = matFactors.Item1;
            var a = matFactors.Item3;
            var term1 = dim / b * rate + 1 - pr / 100 - a / b * rate;
            var term2 = pr / 100 + a / b * rate;
            var term3 = b / dsm;
            return term1 / term2 * term3;
        }

        private static double DiscInternal(DateTime settlement, DateTime maturity, double pr, double redemption, DayCountBasis basis)
        {
            var commonFactors = GetCommonFactors(settlement, maturity, basis);
            var dim = commonFactors.Item1;
            var b = commonFactors.Item2;
            return (-pr / redemption + 1) * b / dim;
        }

        private static double DurationInternal(DateTime settlement, DateTime maturity, double coupon, double yld, double frequency, DayCountBasis basis, bool isMDuration)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var dbc = dc.CoupDaysBS(settlement, maturity, frequency);
            var e = dc.CoupDays(settlement, maturity, frequency);
            var n = dc.CoupNum(settlement, maturity, frequency);
            var dsc = e - dbc;
            var x1 = dsc / e;
            var x2 = x1 + n - 1;
            var x3 = yld / frequency + 1;
            var x4 = Common.Pow(x3, x2);
            Common.Ensure("(yld / frequency + 1)^((dsc / e) + n -1) must be different from 0)", Math.Abs(x4) > Constants.Epsilon);
            var term1 = x2 * 100 / x4;
            var term3 = 100 / x4;
            var aggr = FDuration(coupon, frequency, x1, x3);
            var tuple = Common.AggrBetween(1, (int)n, aggr, new Tuple<double, double>(0, 0));
            var term4 = tuple.Item2;
            var term2 = tuple.Item1;
            var term5 = term1 + term2;
            var term6 = term3 + term4;
            Common.Ensure("term6 must be different from 0)", Math.Abs(term6) > Constants.Epsilon);
            return isMDuration ? term5 / term6 / frequency / x3 : term5 / term6 / frequency;
        }

        private static Tuple<double, double> GetCommonFactors(DateTime settlement, DateTime maturity, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var dayCount = dc;
            var dateTime = settlement;
            var dateTime1 = maturity;
            var dim = dayCount.DaysBetween(dateTime, dateTime1, NumDenumPosition.Numerator);
            var dayCount1 = dc;
            var dateTime2 = settlement;
            var dateTime3 = maturity;
            var b = dayCount1.DaysInYear(dateTime2, dateTime3);
            return new Tuple<double, double>(dim, b);
        }

        private static double IntRateInternal(DateTime settlement, DateTime maturity, double investment, double redemption, DayCountBasis basis)
        {
            var commonFactors = GetCommonFactors(settlement, maturity, basis);
            var dim = commonFactors.Item1;
            var b = commonFactors.Item2;
            return (redemption - investment) / investment * b / dim;
        }

        private static double PriceDiscInternal(DateTime settlement, DateTime maturity, double discount, double redemption, DayCountBasis basis)
        {
            var commonFactors = GetCommonFactors(settlement, maturity, basis);
            var dim = commonFactors.Item1;
            var b = commonFactors.Item2;
            return redemption - discount * redemption * dim / b;
        }

        private static double ReceivedInternal(DateTime settlement, DateTime maturity, double investment, double discount, DayCountBasis basis)
        {
            var commonFactors = GetCommonFactors(settlement, maturity, basis);
            var dim = commonFactors.Item1;
            var b = commonFactors.Item2;
            var discountFactor = discount * dim / b;
            return investment / (1 - discountFactor);
        }

        private static double YieldDiscInternal(DateTime settlement, DateTime maturity, double pr, double redemption, DayCountBasis basis)
        {
            var commonFactors = GetCommonFactors(settlement, maturity, basis);
            var dim = commonFactors.Item1;
            var b = commonFactors.Item2;
            return (redemption - pr) / pr * b / dim;
        }

        private static Tuple<double, double, double, double> GetMatFactors(DateTime settlement, DateTime maturity, DateTime issue, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var b = dc.DaysInYear(issue, settlement);
            var dim = dc.DaysBetween(issue, maturity, NumDenumPosition.Numerator);
            var a = dc.DaysBetween(issue, settlement, NumDenumPosition.Numerator);
            var dsm = dim - a;
            return new Tuple<double, double, double, double>(b, dim, a, dsm);
        }

        private static Tuple<double, DateTime, double, double, double> GetPriceYieldFactors(DateTime settlement, DateTime maturity, double frequency, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var n = dc.CoupNum(settlement, maturity, frequency);
            var pcd = dc.CoupPCD(settlement, maturity, frequency);
            var a = dc.DaysBetween(pcd, settlement, NumDenumPosition.Numerator);
            var e = dc.CoupDays(settlement, maturity, frequency);
            var dsc = e - a;
            return new Tuple<double, DateTime, double, double, double>(n, pcd, a, e, dsc);
        }

        private static double PriceInternal(DateTime settlement, DateTime maturity, double rate, double yld, double redemption, double frequency, DayCountBasis basis)
        {
            var priceYieldFactors = GetPriceYieldFactors(settlement, maturity, frequency, basis);
            var n = priceYieldFactors.Item1;
            var e = priceYieldFactors.Item4;
            var dsc = priceYieldFactors.Item5;
            var a = priceYieldFactors.Item3;
            var coupon = 100 * rate / frequency;
            var accrInt = 100 * rate / frequency * a / e;
            var pvFactor = FPrice(yld, frequency, e, dsc);
            var pvOfRedemption = redemption / pvFactor(n);
            double pvOfCoupons = 0;
            var k = 1;
            var num = (int)n;
            if (num >= k)
            {
                do
                {
                    pvOfCoupons = pvOfCoupons + coupon / pvFactor(k);
                    k++;
                }
                while (k != num + 1);
            }
            return n != 1 ? pvOfRedemption + pvOfCoupons - accrInt : (redemption + coupon) / (1 + dsc / e * yld / frequency) - accrInt;
        }

        private static Func<double, double> FYield(DateTime settlement, DateTime maturity, double rate, double pr, double redemption, double frequency, DayCountBasis basis)
        {
            return yld => PriceInternal(settlement, maturity, rate, yld, redemption, frequency, basis) - pr;
        }

        private static Func<DateTime, DateTime, double> FAccrInt(DateTime issue, DayCountBasis basis, AccrIntCalcMethod calcMethod, DayCount.IDayCounter dc, double freq)
        {
            return (pcd, ncd) =>
                {
                    double days;
                    var firstDate = issue > pcd ? issue : pcd;
                    if (basis == DayCountBasis.UsPsa30_360)
                    {
                        var psaMethod = issue > pcd ? DayCount.Method360Us.ModifyStartDate : DayCount.Method360Us.ModifyBothDates;
                        days = (double)DayCount.DateDiff360US(firstDate, ncd, psaMethod);
                    }
                    else days = dc.DaysBetween(firstDate, ncd, NumDenumPosition.Numerator);

                    double coupDays;
                    if (basis == DayCountBasis.UsPsa30_360) coupDays = (double)DayCount.DateDiff360US(pcd, ncd, DayCount.Method360Us.ModifyBothDates);
                    else
                    {
                        if (basis == DayCountBasis.Actual365) coupDays = 365 / freq;
                        else coupDays = dc.DaysBetween(pcd, ncd, NumDenumPosition.Denumerator);
                    }
                    return issue < pcd ? (double)calcMethod : days / coupDays;
                };
        }

        private static Func<Tuple<double, double>, int, Tuple<double, double>> FDuration(double coupon, double frequency, double x1, double x3)
        {
            return (acc, index) =>
                {
                    var x5 = (double)index - 1 + x1;
                    var x6 = Common.Pow(x3, x5);
                    Common.Ensure("x6 must be different from 0)", Math.Abs(x6) > Constants.Epsilon);
                    var x7 = 100 * coupon / frequency / x6;
                    var tuple = acc;
                    var b = tuple.Item2;
                    var a = tuple.Item1;
                    return new Tuple<double, double>(a + x7 * x5, b + x7);
                };
        }

        private static Func<double, double> FPrice(double yld, double frequency, double e, double dsc)
        {
            return k => Common.Pow(1 + yld / frequency, k - 1 + dsc / e);
        }

        private static double DollarDe(double fractionalDollar, double fraction)
        {
            Common.Ensure("fraction must be more than 0", fraction > 0);
            return Dollar(fractionalDollar, fraction, DollarDeInternal);
        }

        private static double CoupNumber(DateTime d1, DateTime d2, int numMonths, DayCountBasis basis, bool isWholeNumber)
        {
            var mat = d1;
            var tuple = Common.ToTuple(mat);
            var my = tuple.Item1;
            var mm = tuple.Item2;
            var md = tuple.Item3;
            var settl = d2;
            var tuple1 = Common.ToTuple(settl);
            var sy = tuple1.Item1;
            var sm = tuple1.Item2;
            var sd = tuple1.Item3;
            double num = (!isWholeNumber ? 1 : 0);
            var couponsTemp = num;
            var endOfMonthTemp = Common.LastDayOfMonth(my, mm, md);
            var flag = (!endOfMonthTemp && mm != 2);
            var flag1 = (flag && md > 28);
            var flag2 = (flag1 && md < Common.DaysOfMonth(my, mm));
            var flag3 = (!flag2 ? endOfMonthTemp : Common.LastDayOfMonth(sy, sm, sd));
            var endOfMonth = flag3;
            var startDate = DayCount.ChangeMonth(settl, 0, basis, endOfMonth);
            var num1 = (!(settl < startDate) ? couponsTemp : couponsTemp + 1);
            var coupons = num1;
            var date = DayCount.ChangeMonth(startDate, numMonths, basis, endOfMonth);
            var tuple2 = DayCount.DatesAggregate1(date, mat, numMonths, basis, (time, dateTime) => 1, coupons, endOfMonth);
            var result = tuple2.Item3;
            return result;
        }

        private static double DaysBetweenNotNeg(DayCount.IDayCounter dc, DateTime startDate, DateTime endDate)
        {
            var dayCount = dc;
            var dateTime = startDate;
            var dateTime1 = endDate;

            var result = dayCount.DaysBetween(dateTime, dateTime1, NumDenumPosition.Numerator);
            return result <= 0 ? 0 : result;
        }

        private static double DaysBetweenNotNegPsaHack(DateTime startDate, DateTime endDate)
        {
            var result = (double)DayCount.DateDiff360US(startDate, endDate, DayCount.Method360Us.ModifyBothDates);
            return result <= 0 ? 0 : result;
        }

        private static double DaysBetweenNotNegWithHack(DayCount.IDayCounter dc, DateTime startDate, DateTime endDate, DayCountBasis basis)
        {
            return basis != DayCountBasis.UsPsa30_360 ? DaysBetweenNotNeg(dc, startDate, endDate) : DaysBetweenNotNegPsaHack(startDate, endDate);
        }

        private static Func<Tuple<double, double>, int, Tuple<double, double>> FOddFPrice(DateTime settlement, DateTime issue, DayCountBasis basis, DayCount.IDayCounter dc, int numMonthsNeg, double e, DateTime lateCoupon)
        {
            return (t, index) =>
                {
                    var acc = t;
                    var earlyCoupon = DayCount.ChangeMonth(lateCoupon, numMonthsNeg, basis, false);
                    var num = (basis != DayCountBasis.ActualActual ? e : DaysBetweenNotNeg(dc, earlyCoupon, lateCoupon));
                    var dci = (index <= 1 ? DaysBetweenNotNeg(dc, issue, lateCoupon) : num);
                    var startDate = (!(issue > earlyCoupon) ? earlyCoupon : issue);
                    var endDate = (!(settlement < lateCoupon) ? lateCoupon : settlement);
                    var a = DaysBetweenNotNeg(dc, startDate, endDate);
                    lateCoupon = earlyCoupon;
                    var tuple = acc;
                    var dcnl = tuple.Item1;
                    var anl = tuple.Item2;
                    return Tuple.Create(dcnl + dci / num, anl + a / num);
                };
        }

        private static Func<double, int, double> FOddFPriceNegative(double rate, double m, double y, double p1)
        {
            return (acc, index) => acc + 100 * rate / m / Common.Pow(p1, (double)index - 1 + y);
        }

        private static Func<double, int, double> FOddFPricePositive(double rate, double m, double nq, double y, double p1)
        {
            return (acc, index) => acc + 100 * rate / m / Common.Pow(p1, index + nq + y);
        }

        private static Func<double, double> FOddFYield(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double pr, double redemption, double frequency, DayCountBasis basis)
        {
            return yld => pr - OddFPrice(settlement, maturity, issue, firstCoupon, rate, yld, redemption, frequency, basis);
        }

        private static Func<Tuple<double, double, double>, int, Tuple<double, double, double>> FOddLFunc(DateTime settlement, DateTime maturity, DayCountBasis basis, DayCount.IDayCounter dc, int numMonths, double nc, DateTime earlyCoupon)
        {
            return (t, index) =>
                {
                    double a;
                    var acc = t;
                    var lateCoupon = DayCount.ChangeMonth((earlyCoupon), numMonths, basis, false);
                    var nl = DaysBetweenNotNegWithHack(dc, (earlyCoupon), lateCoupon, basis);
                    var num = (index >= (int)nc ? DaysBetweenNotNegWithHack(dc, (earlyCoupon), maturity, basis) : nl);
                    var dci = num;
                    if (!(lateCoupon < settlement)) a = (!(earlyCoupon < settlement) ? 0 : DaysBetweenNotNeg(dc, earlyCoupon, settlement));
                    else a = dci;

                    var dateTime = (!(settlement > earlyCoupon) ? earlyCoupon : settlement);
                    var startDate = dateTime;
                    var dateTime1 = !(maturity < lateCoupon) ? lateCoupon : maturity;
                    var endDate = dateTime1;
                    var dsc = DaysBetweenNotNeg(dc, startDate, endDate);
                    earlyCoupon = lateCoupon;
                    var tuple = acc;
                    var dscnl = tuple.Item3;
                    var dcnl = tuple.Item1;
                    var anl = tuple.Item2;
                    return new Tuple<double, double, double>(dcnl + dci / nl, anl + a / nl, dscnl + dsc / nl);
                };
        }

        private static double OddFPrice(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double yld, double redemption, double frequency, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);

            var numMonths = DayCount.Freq2Months(frequency);
            var numMonthsNeg = -numMonths;
            var num1 = frequency;
            var e = dc.CoupDays(settlement, firstCoupon, num1);

            var n = dc.CoupNum(settlement, maturity, frequency);
            var m = frequency;
            var dfc = DaysBetweenNotNeg(dc, issue, firstCoupon);
            if (dfc >= e)
            {
                var dayCount2 = dc;
                var nc = dayCount2.CoupNum(issue, firstCoupon, frequency);
                var lateCoupon = firstCoupon;
                var aggrFunction = FOddFPrice(settlement, issue, basis, dc, numMonthsNeg, e, lateCoupon);
                var tuple1 = Common.AggrBetween((int)nc, 1, aggrFunction.Invoke, new Tuple<double, double>(0, 0));
                var dcnl = tuple1.Item1;
                var anl = tuple1.Item2;

                double num;
                if (basis == DayCountBasis.Actual360 || basis == DayCountBasis.Actual365)
                {
                    var date = dc.CoupNCD(settlement, firstCoupon, frequency);
                    num = DaysBetweenNotNeg(dc, settlement, date);
                }
                else
                {
                    var date = dc.CoupPCD(settlement, firstCoupon, frequency);
                    var a = dc.DaysBetween(date, settlement, NumDenumPosition.Numerator);
                    num = e - a;
                }
                var nq = CoupNumber(firstCoupon, settlement, numMonths, basis, true);
                n = dc.CoupNum(firstCoupon, maturity, frequency);
                var x = yld / m + 1;
                var y = num / e;
                var term1 = redemption / Common.Pow(x, y + nq + n);
                var term2 = 100 * rate / m * dcnl / Common.Pow(x, nq + y);
                var term3 = Common.AggrBetween<double>(1, (int)n, FOddFPricePositive(rate, m, nq, y, x).Invoke, 0);
                var term4 = 100 * rate / m * anl;
                return term1 + term2 + term3 - term4;
            }
            else
            {
                var dsc = DaysBetweenNotNeg(dc, settlement, firstCoupon);
                var a = DaysBetweenNotNeg(dc, issue, settlement);
                var x = yld / m + 1;
                var y = dsc / e;
                var p1 = x;
                var term1 = redemption / Common.Pow(p1, n - 1 + y);
                var term2 = 100 * rate / m * dfc / e / Common.Pow(p1, y);
                var term3 = Common.AggrBetween<double>(2, (int)n, FOddFPriceNegative(rate, m, y, p1).Invoke, 0);
                var p2 = rate / m;
                var term4 = a / e * p2 * 100;
                return term1 + term2 + term3 - term4;
            }
        }

        private static double OddFYield(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double pr, double redemption, double frequency, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var dayCount = dc;
            var dateTime = settlement;
            var dateTime1 = maturity;

            var years = dayCount.DaysBetween(dateTime, dateTime1, NumDenumPosition.Numerator);

            var px = pr - 100;
            var num = rate * years * 100 - px;
            var denum = px / 4 + years * px / 2 + years * 100;
            var guess = num / denum;
            return Common.FindRoot(FOddFYield(settlement, maturity, issue, firstCoupon, rate, pr, redemption, frequency, basis).Invoke, guess);
        }

        private static double OddLFunc(DateTime settlement, DateTime maturity, DateTime lastInterest, double rate, double prOrYld, double redemption, double frequency, DayCountBasis basis, bool isLPrice)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var m = frequency;
            var numMonths = (int)(12 / frequency);
            var lastCoupon = lastInterest;
            var dayCount = dc;
            var dateTime = lastCoupon;
            var dateTime1 = maturity;
            var num = frequency;
            var nc = dayCount.CoupNum(dateTime, dateTime1, num);
            var earlyCoupon = lastCoupon;
            var aggrFunction = FOddLFunc(settlement, maturity, basis, dc, numMonths, nc, earlyCoupon);
            var tuple = Common.AggrBetween(1, (int)nc, aggrFunction.Invoke, new Tuple<double, double, double>(0, 0, 0));
            var dscnl = tuple.Item3;
            var dcnl = tuple.Item1;
            var anl = tuple.Item2;
            var x = 100 * rate / m;
            var term1 = dcnl * x + redemption;
            if (!isLPrice)
            {
                var term2 = anl * x + prOrYld;
                var term3 = m / dscnl;
                return (term1 - term2) / term2 * term3;
            }
            else
            {
                var term2 = dscnl * prOrYld / m + 1;
                var term3 = anl * x;
                return term1 / term2 - term3;
            }
        }

        private static double OddFPrice(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double yld, double redemption, Frequency frequency, DayCountBasis basis)
        {
            var tuple = Common.ToTuple(maturity);
            var my = tuple.Item1;
            var mm = tuple.Item2;
            var md = tuple.Item3;
            var endMonth = Common.LastDayOfMonth(my, mm, md);
            var numMonths = (int)(12 / (double)frequency);
            var numMonthsNeg = -numMonths;
            var tuple1 = DayCount.FindPcdNcd(DayCount.ChangeMonth(maturity, numMonthsNeg, basis, endMonth), firstCoupon, numMonthsNeg, basis, endMonth);
            var pcd = tuple1.Item1;

            Common.Ensure("maturity and firstCoupon must have the same month and day (except for February when leap years are considered)", pcd == firstCoupon);
            Common.Ensure("maturity must be after firstCoupon", (maturity > firstCoupon));
            Common.Ensure("firstCoupon must be after settlement", (firstCoupon > settlement));
            Common.Ensure("settlement must be after issue", (settlement > issue));
            Common.Ensure("rate must be more than 0", rate >= 0);
            Common.Ensure("yld must be more than 0", yld >= 0);
            Common.Ensure("redemption must be more than 0", redemption >= 0);
            return OddFPrice(settlement, maturity, issue, firstCoupon, rate, yld, redemption, (double)frequency, basis);
        }

        private static double OddFYield(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double pr, double redemption, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after firstCoupon", (maturity > firstCoupon));
            Common.Ensure("firstCoupon must be after settlement", (firstCoupon > settlement));
            Common.Ensure("settlement must be after issue", (settlement > issue));
            Common.Ensure("rate must be more than 0", rate >= 0);
            Common.Ensure("pr must be more than 0", pr >= 0);
            Common.Ensure("redemption must be more than 0", redemption >= 0);
            return OddFYield(settlement, maturity, issue, firstCoupon, rate, pr, redemption, (double)frequency, basis);
        }

        private static double OddLPrice(DateTime settlement, DateTime maturity, DateTime lastInterest, double rate, double yld, double redemption, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("settlement must be after lastInterest", (settlement > lastInterest));
            Common.Ensure("rate must be more than 0", rate >= 0);
            Common.Ensure("yld must be more than 0", yld >= 0);
            Common.Ensure("redemption must be more than 0", redemption >= 0);
            return OddLFunc(settlement, maturity, lastInterest, rate, yld, redemption, (double)frequency, basis, true);
        }

        private static double OddLYield(DateTime settlement, DateTime maturity, DateTime lastInterest, double rate, double pr, double redemption, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("settlement must be after lastInterest", (settlement > lastInterest));
            Common.Ensure("rate must be more than 0", rate >= 0);
            Common.Ensure("pr must be more than 0", pr >= 0);
            Common.Ensure("redemption must be more than 0", redemption >= 0);
            return OddLFunc(settlement, maturity, lastInterest, rate, pr, redemption, (double)frequency, basis, false);
        }

        private static double CumIpmtInternal(double r, double nper, double pv, double startPeriod, double endPeriod, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer)", Common.Raisable(r, nper));
            Common.Ensure("r is not raisable to (per - 1) (r is negative and nper not an integer)", Common.Raisable(r, startPeriod - 1));
            Common.Ensure("pv must be more than 0", pv > 0);
            Common.Ensure("r must be more than 0", r > 0);
            Common.Ensure("nper must be more than 0", nper > 0);
            Common.Ensure("1 * pd + 1 - (1 / (1 + r)^nper) / nper has to be <> 0", Math.Abs(AnnuityCertainPvFactor(r, nper, pd)) > Constants.Epsilon);
            Common.Ensure("startPeriod must be less or equal to endPeriod", startPeriod <= endPeriod);
            Common.Ensure("startPeriod must be more or equal to 1", startPeriod >= 1);
            Common.Ensure("startPeriod and endPeriod must be less or equal to nper", endPeriod <= nper);
            return Common.AggrBetween<double>((int)Common.Ceiling(startPeriod), (int)endPeriod, FCumIpmt(r, nper, pv, pd).Invoke, 0);
        }

        private static double CumPrinc(double r, double nper, double pv, double startPeriod, double endPeriod, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer)", Common.Raisable(r, nper));
            Common.Ensure("r is not raisable to (per - 1) (r is negative and nper not an integer)", Common.Raisable(r, startPeriod - 1));
            Common.Ensure("pv must be more than 0", pv > 0);
            Common.Ensure("r must be more than 0", r > 0);
            Common.Ensure("nper must be more than 0", nper > 0);
            Common.Ensure("1 * pd + 1 - (1 / (1 + r)^nper) / nper has to be <> 0", Math.Abs(AnnuityCertainPvFactor(r, nper, pd)) > Constants.Epsilon);
            Common.Ensure("startPeriod must be less or equal to endPeriod", startPeriod <= endPeriod);
            Common.Ensure("startPeriod must be more or equal to 1", startPeriod >= 1);
            Common.Ensure("startPeriod and endPeriod must be less or equal to nper", endPeriod <= nper);
            return Common.AggrBetween<double>((int)Common.Ceiling(startPeriod), (int)endPeriod, FCumPrinc(r, nper, pv, pd).Invoke, 0);
        }

        private static double Ipmt(double r, double per, double nper, double pv, double fv, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer)", Common.Raisable(r, nper));
            Common.Ensure("r is not raisable to (per - 1) (r is negative and nper not an integer)", Common.Raisable(r, per - 1));
            Common.Ensure("fv or pv need to be different from 0", Math.Abs(fv) < Constants.Epsilon ? pv != 0 : true);
            Common.Ensure("r cannot be -100% when nper is <= 0", r != -1 || (!(!(r != -1 ? false : per > 1) ? false : nper > 1) ? false : pd == PaymentDue.EndOfPeriod));
            Common.Ensure("1 * pd + 1 - (1 / (1 + r)^nper) / nper has to be <> 0", AnnuityCertainPvFactor(r, nper, pd) != 0);
            Common.Ensure("per must be in the range 1 to nper", per < 1 ? false : per <= nper);
            Common.Ensure("nper must be more than 0", nper > 0);
            return !((int)per != 1 ? false : pd == PaymentDue.BeginningOfPeriod) ? (r != -1 ? IpmtInternal(r, per, nper, pv, fv, pd) : -fv) : 0;
        }

        private static double Ispmt(double r, double per, double nper, double pv)
        {
            // constraints dropped here in favor of RadSpreadsheet, meaning that some semantic imaginary situations can be calculated

            //Common.Ensure("per must be in the range 1 to nper", !(per < 1) && per <= nper);
            //Common.Ensure("nper must be more than 0", nper > 0);
            return IspmtInternal(r, per, nper, pv);
        }

        private static double Ppmt(double r, double per, double nper, double pv, double fv, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer)", Common.Raisable(r, nper));
            Common.Ensure("r is not raisable to (per - 1) (r is negative and nper not an integer)", Common.Raisable(r, per - 1));
            Common.Ensure("fv or pv need to be different from 0", fv == 0 ? pv != 0 : true);
            Common.Ensure("r cannot be -100% when nper is <= 0", r != -1 || (!(!(r != -1 ? false : per > 1) ? false : nper > 1) ? false : pd == PaymentDue.EndOfPeriod));
            Common.Ensure("1 * pd + 1 - (1 / (1 + r)^nper) / nper has to be <> 0", AnnuityCertainPvFactor(r, nper, pd) != 0);
            Common.Ensure("per must be in the range 1 to nper", !(per < 1) && per <= nper);
            Common.Ensure("nper must be more than 0", nper > 0);
            return !((int)per == 1 && pd == PaymentDue.BeginningOfPeriod) ? (r != -1 ? PpmtInternal(r, per, nper, pv, fv, pd) : 0) : PmtInternal(r, nper, pv, fv, pd);
        }

        private static double IpmtInternal(double r, double per, double nper, double pv, double fv, PaymentDue pd)
        {
            var result = -(pv * FvFactor(r, per - 1) * r + PmtInternal(r, nper, pv, fv, PaymentDue.EndOfPeriod) * (FvFactor(r, per - 1) - 1));
            return pd != PaymentDue.EndOfPeriod ? result / (1 + r) : result;
        }

        private static double IspmtInternal(double r, double per, double nper, double pv)
        {
            var coupon = -pv * r;
            return coupon - coupon / nper * per;
        }

        private static double PpmtInternal(double r, double per, double nper, double pv, double fv, PaymentDue pd)
        {
            return PmtInternal(r, nper, pv, fv, pd) - IpmtInternal(r, per, nper, pv, fv, pd);
        }

        private static Func<double, int, double> FCumIpmt(double r, double nper, double pv, PaymentDue pd)
        {
            return (acc, per) => acc + Ipmt(r, per, nper, pv, 0, pd);
        }

        private static Func<double, int, double> FCumPrinc(double r, double nper, double pv, PaymentDue pd)
        {
            return (acc, per) => acc + Ppmt(r, per, nper, pv, 0, pd);
        }

        private static double AnnuityCertainFvFactor(double r, double nper, PaymentDue pd)
        {
            return AnnuityCertainPvFactor(r, nper, pd) * FvFactor(r, nper);
        }

        private static double AnnuityCertainPvFactor(double r, double nper, PaymentDue pd)
        {
            return r != 0 ? (1 + r * (double)pd) * (1 - PvFactor(r, nper)) / r : nper;
        }

        private static double Fv(double r, double nper, double pmt, double pv, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer", Common.Raisable(r, nper));
            var flag = r != -1 || (r != -1 ? false : nper > 0);
            Common.Ensure("r cannot be -100% when nper is <= 0", flag);
            //Common.Ensure("pmt or pv need to be different from 0", pmt == 0 ? pv != 0 : true);
            if (pmt == 0 && pv == 0) return 0;
            if (r != -1 ? false : pd == PaymentDue.BeginningOfPeriod) return -(pv * FvFactor(r, nper));
            return !(r != -1 ? false : pd == PaymentDue.EndOfPeriod) ? FvInternal(r, nper, pmt, pv, pd) : -(pv * FvFactor(r, nper) + pmt);
        }

        private static double FvSchedule(double pv, IEnumerable<double> interests)
        {
            var result = pv;
            var nums = interests;
            var enumerator = nums.GetEnumerator();

            using (enumerator)
            {
                while (enumerator.MoveNext()) result = result * (enumerator.Current + 1);
            }
            return result;
        }

        private static double Nper(double r, double pmt, double pv, double fv, PaymentDue pd)
        {
            return (r == 0 ? pmt != 0 : false) ? -(fv + pv) / pmt : NperInternal(r, pmt, pv, fv, pd);
        }

        private static double Pmt(double r, double nper, double pv, double fv, PaymentDue pd)
        {
            bool flag2;
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer", Common.Raisable(r, nper));
            Common.Ensure("fv or pv need to be different from 0", fv == 0 ? pv != 0 : true);
            if (r == -1)
            {
                flag2 = (!(r != -1 ? false : nper > 0) ? false : pd == PaymentDue.EndOfPeriod);
            }
            else flag2 = true;
            Common.Ensure("r cannot be -100% when nper is <= 0", flag2);
            Common.Ensure("1 * pd + 1 - (1 / (1 + r)^nper) / nper has to be <> 0", AnnuityCertainPvFactor(r, nper, pd) != 0);
            return r != -1 ? PmtInternal(r, nper, pv, fv, pd) : -fv;
        }

        private static double Pv(double r, double nper, double pmt, double fv, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is less than -1 and nper not an integer", Common.Raisable(r, nper));

            //Common.Ensure("pmt or fv need to be different from 0", pmt == 0 ? fv != 0 : true);
            Common.Ensure("r cannot be -100%", r != -1);
            // due to data type constraints in Excel it seems the rate cannot go beyond 19.45 see here http://stackoverflow.com/questions/17745852/excels-present-value-pv-function
            if (Math.Abs(r) > 19.45) throw new Exception();
            return PvInternal(r, nper, pmt, fv, pd);
        }

        private static double Rate(double nper, double pmt, double pv, double fv, PaymentDue pd, double guess)
        {
            Common.Ensure("pmt or pv need to be different from 0", pmt == 0 ? pv != 0 : true);
            Common.Ensure("nper needs to be more than 0", nper > 0);
            Common.Ensure("There must be at least a change in sign in pv, fv and pmt", CheckRate(pmt, pv, fv));
            if (fv != 0 ? false : pv == 0) return pmt >= 0 ? 1 : -1;
            var f = FRate(nper, pmt, pv, fv, pd);
            return Common.FindRoot(f.Invoke, guess);
        }

        private static double FvInternal(double r, double nper, double pmt, double pv, PaymentDue pd)
        {
            return -(pv * FvFactor(r, nper) + pmt * AnnuityCertainFvFactor(r, nper, pd));
        }

        private static double FvFactor(double r, double nper)
        {
            return Math.Pow(1 + r, nper);
        }

        private static double NperInternal(double r, double pmt, double pv, double fv, PaymentDue pd)
        {
            var x = NperFactor(r, pmt, -fv, pd) / NperFactor(r, pmt, pv, pd);
            //if (x <= 0 || (r + 1) <= 0) throw new Exception();
            return Common.Ln(x) / Common.Ln(r + 1);
        }

        private static double NperFactor(double r, double pmt, double v, PaymentDue pd)
        {
            return v * r + pmt * (1 + r * (double)pd);
        }

        private static double PmtInternal(double r, double nper, double pv, double fv, PaymentDue pd)
        {
            return -(pv + fv * PvFactor(r, nper)) / AnnuityCertainPvFactor(r, nper, pd);
        }

        private static double PvInternal(double r, double nper, double pmt, double fv, PaymentDue pd)
        {
            return -(fv * PvFactor(r, nper) + pmt * AnnuityCertainPvFactor(r, nper, pd));
        }

        private static double PvFactor(double r, double nper)
        {
            return Math.Pow(1 + r, -nper);
        }

        private static double RaiseIfNonsense(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value)) throw new Exception("Not a number.");
            return value;
        }

        private static Func<double, double> FRate(double nper, double pmt, double pv, double fv, PaymentDue pd)
        {
            return r => Fv(r, nper, pmt, pv, pd) - fv;
        }

        private static bool CheckRate(double x, double y, double z)
        {
            var flag1 = (Common.Sign(x) != Common.Sign(y) || Common.Sign(y) != Common.Sign(z)) && (Common.Sign(x) != Common.Sign(y) ? true : z == 0);
            var flag2 = flag1 && (Common.Sign(x) != Common.Sign(z) ? true : y == 0);
            return flag2 && (Common.Sign(y) != Common.Sign(z) ? true : x == 0);
        }

        private static double DollarFr(double fractionalDollar, double fraction)
        {
            Common.Ensure("fraction must be more than 0", fraction > 0);
            Common.Ensure("10^(ceiling (log10 (floor fraction))) must be different from 0", Common.Pow(10, Common.Ceiling(Common.Log10(Common.Floor(fraction)))) != 0);
            return Dollar(fractionalDollar, fraction, DollarFrInternal);
        }

        private static double Effect(double nominalRate, double npery)
        {
            Common.Ensure("nominal rate must be more than zero", nominalRate > 0);
            Common.Ensure("npery must be more or equal to one", npery >= 1);
            return EffectInternal(nominalRate, npery);
        }

        private static double Nominal(double effectRate, double npery)
        {
            Common.Ensure("effective rate must be more than zero", effectRate > 0);
            Common.Ensure("npery must be more or equal to one", npery >= 1);
            var periods = Common.Floor(npery);
            return (Common.Pow(effectRate + 1, 1 / periods) - 1) * periods;

        }

        private static T Dollar<T>(double fractionalDollar, double fraction, Func<double, double, double, double, T> f)
        {
            var aBase = Common.Floor(fraction);
            var num = (fractionalDollar <= 0 ? Common.Ceiling(fractionalDollar) : Common.Floor(fractionalDollar));
            var dollar = num;
            var remainder = fractionalDollar - dollar;
            var digits = Common.Pow(10, Common.Ceiling(Common.Log10(aBase)));
            return f.Invoke(aBase, dollar, remainder, digits);
        }

        private static double DollarDeInternal(double aBase, double dollar, double remainder, double digits)
        {
            return remainder * digits / aBase + dollar;
        }

        private static double DollarFrInternal(double aBase, double dollar, double remainder, double digits)
        {
            var absDigits = Math.Abs(digits);
            return remainder * aBase / absDigits + dollar;
        }

        private static double EffectInternal(double nominalRate, double npery)
        {
            var periods = Common.Floor(npery);
            return Common.Pow(nominalRate / periods + 1, periods) - 1;
        }

        private static double BillPriceInternal(DateTime settlement, DateTime maturity, double discount)
        {
            var dsm = GetDsm(settlement, maturity, DayCountBasis.ActualActual);
            return 100 * (1 - discount * dsm / 360);
        }

        private static double BillYieldInternal(DateTime settlement, DateTime maturity, double pr)
        {
            var dsm = GetDsm(settlement, maturity, DayCountBasis.ActualActual);
            return (100 - pr) / pr * 360 / dsm;
        }

        private static double BillEq(DateTime settlement, DateTime maturity, double discount)
        {
            var dsm = GetDsm(settlement, maturity, DayCountBasis.Actual360);
            var price = (100 - discount * 100 * dsm / 360) / 100;
            var days = dsm != 366 ? 365 : 366;
            var tempTerm2 = Common.Pow(dsm / days, 2) - (2 * dsm / days - 1) * (1 - 1 / price);
            //Common.Ensure("(dsm / days)^2 - (2. * dsm / days - 1.) * (1. - 1. / (100. - discount * 100. * dsm / 360.) / 100.) must be positive", tempTerm2 >= 0);
            Common.Ensure("2. * dsm / days - 1. must be different from 0", 2 * dsm / days - 1 != 0);
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("maturity must be less than one year after settlement", (maturity <= Common.AddYears(settlement, 1)));
            Common.Ensure("investment must be more than 0", discount > 0);

            if (dsm <= 182) return 365 * discount / (360 - discount * dsm);
            return 2 * (Common.Sqr(tempTerm2) - dsm / days) / (2 * dsm / days - 1);
        }

        private static double BillPrice(DateTime settlement, DateTime maturity, double discount)
        {
            var dsm = GetDsm(settlement, maturity, DayCountBasis.ActualActual);
            Common.Ensure("a result less than zero triggers an exception", 100 * (1 - discount * dsm / 360) > 0);
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("maturity must be less than one year after settlement", (maturity <= Common.AddYears(settlement, 1)));
            Common.Ensure("discount must be more than 0", discount > 0);
            return BillPriceInternal(settlement, maturity, discount);
        }

        private static double BillYield(DateTime settlement, DateTime maturity, double pr)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("maturity must be less than one year after settlement", (maturity <= Common.AddYears(settlement, 1)));
            Common.Ensure("pr must be more than 0", pr > 0);
            return BillYieldInternal(settlement, maturity, pr);
        }

        private static double GetDsm(DateTime settlement, DateTime maturity, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var dayCount = dc;
            var dateTime = settlement;
            var dateTime1 = maturity;
            return dayCount.DaysBetween(dateTime, dateTime1, NumDenumPosition.Numerator);
        }

        private static double Irr(IEnumerable<double> cfs, double guess)
        {
            Common.Ensure("There must be one positive and one negative cash flow", ValidCfs(cfs));
            return Common.FindRoot(d => NpvInternal(d, cfs), guess);
        }

        private static double Mirr(IEnumerable<double> cfs, double financeRate, double reinvestRate)
        {
            Common.Ensure("financeRate cannot be -100%", financeRate != -1);
            Common.Ensure("reinvestRate cannot be -100%", reinvestRate != -1);
            Common.Ensure("cfs must contain more than one cashflow", cfs.Count() != 1);
            Common.Ensure("The NPV calculated using financeRate and the negative cashflows in cfs must be different from zero", NpvInternal(financeRate, cfs.Map(cf => cf >= 0 ? 0 : cf)) != 0);
            return MirrInternal(cfs, financeRate, reinvestRate);
        }

        private static double Npv(double r, IEnumerable<double> cfs)
        {
            Common.Ensure("r cannot be -100%", r != -1);
            return NpvInternal(r, cfs);
        }

        private static double Xirr(IEnumerable<double> cfs, IEnumerable<DateTime> dates, double guess)
        {
            Common.Ensure("There must be one positive and one negative cash flow", ValidCfs(cfs));
            Common.Ensure("In dates, one date is less than the first date", dates.ToList().Any(time => (time <= dates.First())));
            Common.Ensure("cfs and dates must have the same length", cfs.Count() == dates.Count());
            return Common.FindRoot(r => XnpvInternal(r, cfs, dates), guess);
        }

        private static double Xnpv(double r, IEnumerable<double> cfs, IEnumerable<DateTime> dates)
        {
            Common.Ensure("r cannot be -100%", r != -1);
            Common.Ensure("In dates, one date is less than the first date", dates.ToList().Any(time => (time <= dates.First())));
            Common.Ensure("cfs and dates must have the same length", cfs.Count() == dates.Count());
            return XnpvInternal(r, cfs, dates);
        }

        private static double MirrInternal(IEnumerable<double> cfs, double financeRate, double reinvestRate)
        {
            var n = (double)cfs.Count();
            var cfsl = cfs.ToList();
            var positives = cfsl.Map(input => Math.Max(input, 0));
            var negatives = cfsl.Map(input => Math.Min(input, 0));
            return Math.Pow(-NpvInternal(reinvestRate, positives) * Math.Pow(1 + reinvestRate, n) / (NpvInternal(financeRate, negatives) * (1 + financeRate)), 1 / (n - 1)) - 1;
        }

        private static double NpvInternal(double r, IEnumerable<double> cfs)
        {
            double nums = 0;
            var cfsl = cfs.ToList();
            for (var i = 0; i < cfsl.Count(); i++) nums += cfsl[i] * PvFactor(r, (double)(i + 1));
            return nums;
        }

        private static bool ValidCfs(IEnumerable<double> cfs)
        {
            return ValidCfs(cfs, false, false);
        }

        private static bool ValidCfs(IEnumerable<double> cfs, bool pos, bool neg)
        {
            if (pos && neg) return true;
            if (cfs == null || !cfs.Any()) return false;
            var l = new FunctionalList<double>(cfs);
            var h = l.Head;
            var t = l.Tail;
            return h > 0 ? ValidCfs(t, true, neg) : ValidCfs(t, pos, true);
        }

        private static double XnpvInternal(double r, IEnumerable<double> cfs, IEnumerable<DateTime> dates)
        {
            var dateTimes = dates as IList<DateTime> ?? dates.ToList();
            var d0 = dateTimes.First();
            var mapped = dateTimes.Zip(cfs, (d, cf) => cf / Math.Pow(1 + r, (double)Common.Days(d, d0) / 365));
            return mapped.Sum();
        }

        static class Depreciation
        {
            private static double DeprBetweenPeriods(double cost, double salvage, double life, double startPeriod, double endPeriod, double factor, bool straightLine)
            {
                return TotalDepr(cost, salvage, life, endPeriod, factor, straightLine) - TotalDepr(cost, salvage, life, startPeriod, factor, straightLine);
            }