# FuzzyMath
A lightweight library for fuzzy arithmetic based on operations over the α-cuts of piecewise linear fuzzy numbers.

### Installation
[Install via NuGet](https://www.nuget.org/packages/FuzzyMath/)

### License
The MIT License (MIT)

## Basic usage

### Fuzzy Number
The easiest way to create a (piecewise linear) fuzzy number is to use a `FuzzyNumberFactory` service. The constructor takes two optional arguments. The first one is a number of α-cuts, which the fuzzy numbers are made of. The second one is an epsilon to deal with floating points rounding errors.
```c#
var factory = new FuzzyNumberFactory(6);

FuzzyNumber A = factory.createTriangular(2, 3, 5, 7);
FuzzyNumber B = factory.createTrapezoidal(5, 8, 9);
FuzzyNumber C = factory.createCrisp(7);
```

Each α-cut (`Interval`) of the fuzzy number is accessible from `FuzzyNumber.AlphaCuts` property (`IList<Interval>`) by its index
```c#
Interval a = A.AlphaCuts[0]; // kernel
```

or you can get an α-cut by its membership value [0-1]
```c#
Interval b = B.GetAlhpaCut(.75);
```

To get a degree of membership μ(x) of value x (`double`) use
```c#
double m1 = B.GetMembership(4); // returns 0
double m2 = B.GetMembership(7.1) // returns .7
double m3 = B.GetMembership(8) // returns 1
```

### Basic arithmetic
Operators `+-*/` are overloaded, so you can use the fuzzy numbers created above as if they were `double`s
```c#
FuzzyNumber D = 2 * C - (2.5 + A / B);
```
Shapes of fuzzy numbers created above:
![Fuzzy numbers A, B, C](https://cloud.githubusercontent.com/assets/7131153/14786404/03e53cc0-0aff-11e6-818e-9dee1ad5a048.PNG)

### Operations
Every operation over fuzzy number(s) is perfomed as the same operation over the set of corresponding α-cuts (`Interval`s). That is exactly what the static method `FuzzyNumber.Map()` does.

For example operations
```c#
var E = -1 * D;
var F = D + E;
```
are actualy performed as
```c#
var E = FuzzyNumber.Map(D, d => -1 * d); // unary operation
var F = FuzzyNumber.Map(D, E, (d, e) => d + e); // binary operation
```

