# FuzzyMath
A lightweight library for fuzzy arithmetic.

## Usage

### Fuzzy Number
The easiest way to create a fuzzy number is to use a `FuzzyNumberFactory` service. The constructor takes two optional arguments. The first one is number of aplha-cuts, which the numbers are made of. The second one is an epsilon to deal with floating poits rounding errors.
```c#
var factory = new FuzzyNumberFactory(6);

FuzzyNumber A = factory.createTriangular(1, 2, 3);
FuzzyNumber B = factory.createTrapezoidal(2, 4, 5, 6);
FuzzyNumber C = factory.createCrisp(-0.5);
```

TODO

## License
The MIT License (MIT)
