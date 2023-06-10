---
uid: reusing-code
level: 300
---

# Reusing code among aspects and templates

As you develop an understanding of how Metalama Aspects are created and how powerful they can be so it will become apparent that many of the aspects that you create may well have some common functionality, indeed they may even use snippets of code that are exactly the same.  One of the principle puposes behind Metalama is to encourage the use of Clean Code and clearly this rather goes against the overring principle that Metalama is seeking to promote.
The purpose of this section is to illustrate some of those places where it would make sense to consider reusing code and to point out why in some cases you shouldn't.

## [Compile Time] v [Template]

The first thing to be clear about is how Metalama differentiates code because it has a strict bearing on what can and cannot be reused.

### [CompileTime]

//add description here along with and obvious example//

### [Template]

//add description here along with and obvious example//

## Plan Aspects with reuse in mind

Thinking of each aspect in the singular as it is designed, created and finally tested to ensure that it does what was planned for it is a relatively straightforward process.  However during the planning stage it pays to think about whether parts of the aspect might prove to be beneficial elsewhere.  Logging makes a good case in point to illustrate this.

Creating an aspect, or a series of aspects that when combined will cover 90 or even 95% of possible logging requirements is straightforward. The remaining few percent represent those edge cases where the more generic approach used elsewhere is not appropriate. These situations require specific messages that a more generic approach cannot provide.

This situation can in part be solved by ordering the way that aspects are added to code (refer to the section on [Ordering Aspects](https://doc.metalama.net/conceptual/aspects/ordering)) but that can still lead to code duplication and even, in extreme cases, conflicts between aspects.

### Recognise reusable code

Currently aspects can share ```[CompileTime]``` methods but not ```[Template]``` methods.  It follows therefore that recognising the difference between the two is fundamental to realising where code can and very probably should be considered suitable for reuse.

// block below should be a specific note block

>The principle of DRY (Don't Repeat Yourself) coding is particularly relevant here but it will not always be apparent at the time that code is written that it either can or will be reused at some point in the future.

>Notwithstanding it is undoubtedly true that maintaing a single source of code is far easier in the longer term than than trying to maintain exactly the same code repeat across a series of disparate aspects.

>This becomes even more relevant if aspects are being created for use across development teams or are part of open source projects.


### Support the lowest common denominator


Wherever possible make sure that the code that clearly fits the definition of reusable in relation to aspects,  targets the lowest common framework (ideally .Net Standard 2.0) to ensure that aspects which use it can have the greatest reach possible.  

Where that is not possible then consider separating reusable code according to the lowest framework that it can support.  

The overriding principle is that the code remains clean and in being so it remains easier to maintain.

If creating reusable libraries for internal teams or Open Source projects ensure that they are also properly documented. Code that can be reused is at its best when it is clear to all who use it exactly what it is intended to do and how it should be used.
