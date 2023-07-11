---
uid: testing
level: 300
---

# Testing aspects

There are three complementary strategies to test your aspects. The first strategy should provide sufficient coverage for the most common scenarios.

<table>
    <tr>
        <th>Article</th>
        <th>Description</th>
    </tr>
    <tr>
        <td>
            <xref:aspect-testing>
        </td>
        <td>
             These tests verify that the aspect transforms code or reports errors and warnings as expected. In compile-time tests, the transformed code is not executed.
        </td>
    </tr>
    <tr>
        <td>
            <xref:run-time-testing>
        </td>
        <td>
        These tests confirm the run-time behavior of the aspect. In this approach, you apply your aspect to some test target code and evaluate the _behavior_ of the combination of the aspect and the target code by executing the transformed code in a unit test and assessing its run-time behavior. For this approach, you can use a conventional Xunit project or any other testing framework, as there is nothing specific to Metalama.
        </td>
    </tr>
    <tr>
        <td>
            <xref:compile-time-testing>
        </td>
        <td>
            These tests are traditional unit tests of the compile-time logic used by the aspects, without executing the aspects themselves.
        </td>
    </tr>
</table>


