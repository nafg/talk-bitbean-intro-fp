// Imperative, mutable, and side-effecting
function fibs1(len: number): void {
    var a = 0
    var b = 1
    var i = 0
    while (i < len) {
        // Can we reorder any of these statements?
        // Can we inline any of them?
        // What refactorings are safe?
        process.stdout.write(a.toString())
        if (i + 1 < len) process.stdout.write(", ")
        const c = a + b
        a = b
        b = c
        i++
    }
}

fibs1(20)

console.log()

// Separate out the side effects
function fibs2(len: number) {
    var a = 0
    var b = 1
    const array = new Array<number>(len)
    var i = 0
    while (i < len) {
        array[i] = a
        const c = a + b
        a = b
        b = c
        i++
    }
    return array
}

console.log(fibs2(20).join(", "))

// Move mutability to the top: variables are not themselves mutable
function fibs3(len: number) {
    var a = 0
    var b = 1
    var list = new Array<number>()
    var i = 0
    while (i < len) {
        list = [...list, a]
        const c = a + b
        a = b
        b = c
        i++
    }
    return list
}

console.log(fibs3(20).join(', '))

// Convert to functional
// 1. State we need to read inside the loop becomes a parameter
// 2. State we need to read after the loop becomes the return value
// 3. The looping condition determines whether to continue recursing
// 4. Initial state becomes arguments to outer call, or argument defaults
function fib4(len: number) {
    function loop(list: Array<number> = [], i: number = 0, a: number = 0, b: number = 1): Array<number> {
        if (i < len)
            return loop([...list, a], i + 1, b, a + b)
        else
            return list
    }

    return loop()
}

console.log(fib4(20).join(', '))
