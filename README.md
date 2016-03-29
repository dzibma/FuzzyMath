# FuzzyMath
A lightweight library for fuzzy arithmetic.

## Usage

### Fuzzy Number
The easiest way to create a fuzzy number is to use a `FuzzyNumberFactory` service. The constructor takes two optional arguments. The first one is a number of aplha-cuts, which the fuzzy numbers are made of. The second one is an epsilon to deal with floating points rounding errors.
```c#
var factory = new FuzzyNumberFactory(6);

FuzzyNumber A = factory.createTriangular(2, 3, 5, 7);
FuzzyNumber B = factory.createTrapezoidal(5, 8, 9);
FuzzyNumber C = factory.createCrisp(7);
```
### Basic arithmetic
Operators `+-*/` are overloaded, so you can use the fuzzy numbers created above as if they were `double`s.
```c#
FuzzyNumber D = -2 * C + A / B;
```

![Fuzzy numbers A, B, C](https://cloud.githubusercontent.com/assets/7131153/14114997/8fe492a6-f5d9-11e5-8bdc-bf34be7a653a.PNG)

## License
The MIT License (MIT)
