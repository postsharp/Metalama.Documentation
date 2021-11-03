---
uid: Caravela.Framework.Validation
summary: *content
---
This is namespace allows you to validate your code, the code that uses your aspects, or the code that references the code
that uses your aspects.

This namespace is not yet implemented.

Validators execute _after_ all aspects have been applied to the compilation. To validate whether an aspect is eligible for
a declaration (which involves validating the compilation _before_ the aspect has been applied), implement the 
<xref:Caravela.Framework.Eligibility.IEligible%601.BuildEligibility%2A> 
aspect method.

