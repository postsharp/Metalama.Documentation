---
uid: testing
level: 300
---

# Testing aspects

There are three complementary strategies to test your aspects. For the most common scenarios, the first strategy should provide a sufficient coverage.

<table>
    <tr>
        <th>Article</th>
        <th>Description</th>
    <tr>
    <tr>
        <td>
            <xref:aspect-testing>
        </td>
        <td>
             These tests verify that the aspect transforms some target code as expected, or emits errors and warnings as expected. With compile-time tests, the transformed code is not executed.
        </td>
    </tr>
    <tr>
        <td>
            <xref:run-time-testing>
        </td>
        <td>
        These tests verify the run-time behavior of the aspect. With this approach, you apply your aspect to some test target code, and test the _behavior_ of the combination of the aspect and the target code by executing the transformed code in a unit test, and evaluating assertions regarding its run-time behavior. For this approach, you can use a conventional Xunit project, or use any other testing framework because there is nothing specific to Metalama.
        </td>
    </tr>
    <tr>
        <td>
            <xref:compile-time-testing>
        </td>
        <td>
            These tests are classic unit tests of the compile-time logic used by the aspects, without executing the aspects themselves.
        </td>
    </tr>
</table>

