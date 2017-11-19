package fibonacci

object Fibonacci {
  def main(args: Array[String]): Unit = {
    // Imperative, mutable, and side-effecting
    def fibs1(len: Int): Unit = {
      var a = 0
      var b = 1
      var i = 0
      while (i < len) {
        // Can we reorder any of these statements?
        // Can we inline any of them?
        // What refactorings are safe?
        print(a)
        if (i + 1 < len) print(", ")
        val c = a + b
        a = b
        b = c
        i += 1
      }
    }

    fibs1(20)

    println()

    // Separate out the side effects
    def fibs2(len: Int) = {
      var a = 0
      var b = 1
      val array = new Array[Int](len)
      var i = 0
      while (i < len) {
        array(i) = a
        val c = a + b
        a = b
        b = c
        i += 1
      }
      array
    }

    println(fibs2(20).mkString(", "))

    // Move mutability to the top: variables are not themselves mutable
    def fibs3(len: Int) = {
      var a = 0
      var b = 1
      var list = Seq.empty[Int]
      var i = 0
      while (i < len) {
        list = list :+ a
        val c = a + b
        a = b
        b = c
        i += 1
      }
      list
    }

    println(fibs3(20).mkString(", "))

    // Convert to functional
    // 1. State we need to read inside the loop becomes a parameter
    // 2. State we need to read after the loop becomes the return value
    // 3. The looping condition determines whether to continue recursing
    // 4. Initial state becomes arguments to outer call, or argument defaults
    def fib4(len: Int) = {
      def loop(list: Seq[Int] = Nil, i: Int = 0, a: Int = 0, b: Int = 1): Seq[Int] =
        if (i < len)
          loop(list :+ a, i + 1, b, a + b)
        else
          list

      loop()
    }

    println(fib4(20).mkString(", "))
  }
}
