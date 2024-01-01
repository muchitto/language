# Language

Language is an optionally typed language (though it is by default a statically typed language). It can compile to C and Javascript (and maybe in the future CIL).

## Constructs

### Variable Declaration

Variables are declared like so

    var variableName int      // The type is added after the name
    var variableName = 10     // The type can be inferred from the initial value
    var variableName int = 10 // Both can be defined also

Variables can also be dynamically typed by adding a question mark after the `var` keyword:

    var? variableName = 10
    variableName = "a string"

    // It is basically syntactic sugar for
    var variableName dynamic = 10

Constants are declared by using the `let` keyword.

    let variableName = 10

### Functions

Functions are declared using the `func` keyword and ended with the `end` keyword.

    func name(arg1, arg2, arg3)
        // ...
    end

Function arguments can be dynamically typed by using the questionmark after the argument name, but can be typed by adding a type identifier after the name.

    func name(arg1 int, arg2 CustomType)
        // ...
    end

Function can have a return type by adding a type in the end.

    func name() int
        return 10
    end

Functions are called by giving the argument name and the value. Argument names must be given

    func name(arg1 int) int
        // ...
    end

    name(arg1: 10)

Functions can be overloaded by argument names and argument types.

    func name(arg1 int) int
        // ...
    end

    name(arg1: 10)

    func name(arg1 string) int
        // ...
    end

    name(arg1: "test")

    func name(arg2 int) int
        // ...
    end

    name(arg2: 10)

    func test(what, another func(int) void)
        another(10)
    end

    test(func (num)

    end)

    test ("haha") in num

    end

### Structures

Structures are defined like so

    struct Test
        var field1 int
        var field2 = 10

        init() // Constructor
            self.field1 = 10 // the struct itself is accessed by using the self
        end

        func method()
            // ...
        end

        deinit() // Destructor

        end
    end

Structures can have fields and methods. Structures have value semantics.

### Classes

Classes are like structures, but they have reference semantics.

    class Test
        var field1 int
        var field2 = 10

        init() // Constructor
            self.field1 = 10 // the struct itself is accessed
                             // by using the self
        end

        func method()
        end

        deinit() // Destructor

        end
    end

### Interfaces

Interfaces can define functions or even fields that a type must implement to be able to fulfill.

Interfaces are defined like so

    interface Functionality
        func thisMustBeImplemented()
    end

### Generics

Generic types can be used in structs and functions.

    struct Test<T>
        var var1 T
    end

    struct Test<T where Numeric>
        var var1 T
    end

### Enums

Enums are defined as such:

    enum Test
        value1
        value2
        value3
    end

Enums can hold values also.

    enum Test
        value1(data1 int, data2 string)
        value2(data1 float)
    end

Enums are initialized with

    // ...

    var var1 = Test.value1(data1: 10, data2: "test")

If the type of the the variable is known, the enum typename can be omitted

    var var1 Test = .value1(data1: 10, data2: "test")

### Error handling

Error handling is handled by exception handling.

A function can throw an error if the function is marked as throwy

    func test() throws
        throw Error.error
    end

If you want to call a function that throws, you must use the try keyword in front of the function call

    // ...

    try test()

An error can be handled in a try catch clause

    // ...

    try
        try test()
    catch Error.error
        // handle error
    end

The compiler should track all the errors that a function can throw and check that every one is handled at some point and if not, throw an error.

### More about the types

Functions, structs and classes can be entered in any order.

#### Option types

Option types are the type `Option<Type>`. It has to be "opened" up before it can be used as the actual value.

You can define an option type by appendixing the type name with a question mark

    struct Test
        func test()
            // ...
        end
    end

    func test(arg1 Test?)
        arg1.test() // This should error as you cannot view
                    // into the Option type directly
    end

You can null check the argument using

    // ...

    func test(arg1 Test?)
        if arg1.field1 != null
            arg1.test() // This should work
        end
    end

#### Dynamic typing vs static typing

Every time a dynamically typed variable is used in a statically typed context, a runtime typecheck is added to that context.

For instance when a statically typed function argument receives a dynamically typed variable, a typecheck is added.

    func test(arg1 int)
        // ...
    end

    var? var1 = 10
    // Before the test function is called, a runtime typecheck is added here
    test(var1)

Or if a dynamically typed variable is used in a statically typed expression, a typecheck is added.

    var? var1 = "test"
    // runtime typecheck added
    var var2 int = 10 * var1

Basically at every possible point when dynamically typed and statically typed contexts collide, runtime checks are added.

## Externs

The language should be able to interop with different language depending on the backend.

Javascript should be able to just call external Javascript functions and C should be able to call C functions.

### Javascript

There should be different outputs for Node and vanilla Javascript. For instance no file handling functions in vanilla Javascript.

    extern javascriptFunction()

###

## Backends

The language has many backends for different usecases. You can build for instance games that run on the browser (by exporting Javascript) or on the desktop by exporting C.

Sometimes in the future, a LLVM backend and CIL backend could be thought about.

### Javascript

The code should transpile straight to Javascript so it will inherit Javascript garbage collection characteristics.

### C

This is the main desktop backend. It uses automatic reference counting for memory management. It will export standalone executables.
