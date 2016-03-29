# FuzzyMath
A lightweight library for fuzzy arithmetic.

## Usage

### Fuzzy Number
The easiest way to create a fuzzy number is to use a `FuzzyNumberFactory` service. The constructor takes two optional arguments. The first one is number of aplha-cuts, which the numbers are made of. The second one is an epsilon to deal with floating poits rounding errors.
```c#
var factory = new FuzzyNumberFactory(6);

FuzzyNumber A = factory.createTriangular(2, 3, 5, 7);
FuzzyNumber B = factory.createTrapezoidal(5, 8, 9);
FuzzyNumber C = factory.createCrisp(7);
```

![Fuzzy numbers A, B, C](https://cloud.githubusercontent.com/assets/7131153/14114119/17a45572-f5d6-11e5-916d-452b434f4650.PNG)

### Basic arithmetic
```c#
FuzzyNumber D = -2 * C + A / B;
```

![Fuzzy number D](https://cloud.githubusercontent.com/assets/7131153/14114121/18576d74-f5d6-11e5-87f0-4a075f1b1d5d.PNG)

## License
The MIT License (MIT)
