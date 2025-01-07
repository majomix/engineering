namespace LeetCode.Cracking.MathAndLogicPuzzles;

/// <summary>
/// You have a basketball hoop and someone says you can play one of two following games.
/// Game 1: You get one shot to make the hoop.
/// Game 2: You get three shots and you have to make two of three shots.
/// 
/// If p is the probability of making a particular shot, for which values of p should you pick one game or the other?
/// 
/// Solution:
/// 1. Probability of winning Game 1 is p.
/// 2. Probability of winning Game 2 is 2 of 3 shots OR 3 out of 3 shots which are:
///    nyy OR yny OR yyn OR yyy
///    (1-p)*p*p+p*(1-p)*p+p*p*(1-p)+p*p*p = 3p^2-2p^3
///    
/// To pick Game 1 means P(G1)>P(G2), so
/// p > 3p^2-2p^3
/// (2p-1)(p-1) > 0 where p1 = 0.5, p2 = 1. However, P can be only in range <0,1>.
/// 
///            0.5    1
/// 2p-1     -     +     +
/// p-1      -     -     +
///          +     -     +
///          Y     X     X
/// To choose Game 1, P should be (0,0.5).
/// To choose Game 2, P should be (0.5,1).
/// 
/// For corner points 0, 0.5 and 1 the two expressions are equal. 
/// </summary>
