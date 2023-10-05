---
uid: contract-types
---

# List of contract attributes



You can choose from the following contract attributes:


## Nullablity contracts

### [NotNull]

The <xref:Metalama.Patterns.Contracts.NotNullAttribute> contract simply verifies that the value is not `null`.

#### Example: [NotNull]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/NotNullContract.cs]

### [Required]

Just as <xref:Metalama.Patterns.Contracts.NotNullAttribute> contract, <xref:Metalama.Patterns.Contracts.RequiredAttribute> also simply verifies that the value is not `null`, but it also requires the string to be non-emtpty.


#### Example: [Required]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/RequiredContract.cs]


> [!WARNING]
> All contracts below accept `null` values without validating them.


## String contracts

### [NotEmpty] 

The <xref:Metalama.Patterns.Contracts.NotEmptyAttribute> contract requires the string to be non-empty. Note that this contract does not require the string to be non-null. If you want to forbid both null and empty strings, use the <xref:Metalama.Patterns.Contracts.RequiredAttribute> constraint.

#### Example: [NotEmpty] vs [NotNull] vs [Required]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/NotEmptyContract.cs]

### [CreditCard]

The <xref:Metalama.Patterns.Contracts.CreditCardAttribute> contract verifies that the string is a valid credit card number. 

It calls the delegate exposed by the <xref:Metalama.Patterns.Contracts.ContractHelpers.IsValidCreditCardNumber?text=ContractHelpers.IsValidCreditCardNumber> property. You can replace this delegate by your own implementation.

#### Example: [CreditCard]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/CreditCardContract.cs]

### [Email], [Phone], and [Url]

You can use the <xref:Metalama.Patterns.Contracts.PhoneAttribute>, <xref:Metalama.Patterns.Contracts.EmailAttribute> and <xref:Metalama.Patterns.Contracts.UrlAttribute> to validate strings against well-known regular expressions.

You can also customize these regular expressions by setting the <xref:Metalama.Patterns.Contracts.ContractHelpers.PhoneRegex>, <xref:Metalama.Patterns.Contracts.ContractHelpers.EmailRegex>, <xref:Metalama.Patterns.Contracts.ContractHelpers.UrlRegex> properties of the <xref:Metalama.Patterns.Contracts.ContractHelpers> class.

#### Example: [Email], [Phone], and [Url]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/WellKnownRegexContracts.cs]

### Custom regular expressions

The  <xref:Metalama.Patterns.Contracts.RegularExpressionAttribute> contract validates a string against an arbitrary regular expression. Instead of using directly the <xref:Metalama.Patterns.Contracts.RegularExpressionAttribute> class, it may be preferable, if the same regular expression is to be used many times, to create a derived class.

#### Example: custom regular expression

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/CustomRegexContract.cs]

### String length

The  <xref:Metalama.Patterns.Contracts.StringLengthAttribute> contract validates that a the length of a string is in a given range.


#### Example: [StringLength]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/StringLengthContract.cs]


## Enum contracts

The <xref:Metalama.Patterns.Contracts.EnumDataTypeAttribute> contract can validate values of type `string`, `object`, or of any integer type. It throws an exception is the value is not a valid value for the given `enum` type.

### Example: [EnumDataType]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/EnumDataTypeContract.cs]


## Numeric contracts


You can use one of the following contracts to verify that a value is within a specified range:

| Attribute                                                 | Description |
|-----------------------------------------------------------|-------------|
| <xref:Metalama.Patterns.Contracts.LessThanAttribute>     | Verifies that the value is less than or equal to the given maximum. 
| <xref:Metalama.Patterns.Contracts.GreaterThanAttribute>  |  Verifies that the value is greater than or equal to the given minimum. 
| <xref:Metalama.Patterns.Contracts.NegativeAttribute>     | Verifies that the value is less than or equal to zero. 
| <xref:Metalama.Patterns.Contracts.PositiveAttribute>     | Verifies that the value is greater than or equal to zero. 
| <xref:Metalama.Patterns.Contracts.RangeAttribute>     | Verifies that the value is greater than or equal a given minimum and less than or equal to a given maximum.
| <xref:Metalama.Patterns.Contracts.StrictlyLessThanAttribute>     | Verifies that the value is strictly less than to the given maximum. 
| <xref:Metalama.Patterns.Contracts.StrictlyGreaterThanAttribute>  |  Verifies that the value is strictly greater than the given minimum. 
| <xref:Metalama.Patterns.Contracts.StrictlyNegativeAttribute>     | Verifies that the value is strictly less than zero. 
| <xref:Metalama.Patterns.Contracts.StrictlyPositiveAttribute>     | Verifies that the value is strictly greater than to zero. 
| <xref:Metalama.Patterns.Contracts.StrictRangeAttribute>     | Verifies that the value is strictly greater than a given minimum and strictly less than a given maximum.

### Example: numeric constracts

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/NumericContracts.cs]


## Collections contracts

The <xref:Metalama.Patterns.Contracts.NotEmptyAttribute> contract can be used on any collection, including arrays or immutable arrays. It requires the collection or the array to contain at least one element.

 Note that this contract does not require the collection object to be non-null (or, in case of immutable arrays, default ones). If you want to forbid both null and empty collections, also add the <xref:Metalama.Patterns.Contracts.NotNullAttribute> constraint.

### Example: [NotEmpty] on collection

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/NotEmptyCollectionContract.cs]
