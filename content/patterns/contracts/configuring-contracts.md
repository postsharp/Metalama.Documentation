---
uid: configuring-contracts
---

# Configuring contracts

There are two kinds of configuration options: the compile-time ones, and the run-time ones. Compile-time options affect the code generation and must be configured using a fabric, while run-time settings must be configured at run-time during application start up.

Let's start with run-tine settings.


## Changing the validation logic

Some contracts use flexible run-time settings to perform their validation. These settings are settable properties of the <xref:Metalama.Patterns.Contracts.ContractHelpers> class. 

| Aspect | Property | Description |
|-----|----|-----|
| <xref:Metalama.Patterns.Contracts.CreditCardAttribute> | <xref:Metalama.Patterns.Contracts.ContractHelpers.IsValidCreditCardNumber> | The `Func<string?, bool>` validating the string as a credit card number. |
| <xref:Metalama.Patterns.Contracts.EmailAttribute>  | <xref:Metalama.Patterns.Contracts.ContractHelpers.EmailRegex> | A regular expression validating the string as an email address. |
| <xref:Metalama.Patterns.Contracts.UrlAttribute>  | <xref:Metalama.Patterns.Contracts.ContractHelpers.UrlRegex> | A regular expression validating the string as an URL. |
| <xref:Metalama.Patterns.Contracts.PhoneAttribute>  | <xref:Metalama.Patterns.Contracts.ContractHelpers.PhoneRegex> | A regular expression validating the string as a phone number. |

You can set any of this property during the initialization sequence of your application, and the change will affect the whole application.

There is currently no way to modify these settings for a given namespace or type of your application.

## Changing compile-time options

All other configurable options are compile-time ones. They are represented by the <xref:Metalama.Patterns.Contracts.ContractOptions> class. They can be configured granularily from a fabric using the configuration framework described in <xref:fabrics-configuration>

### Enabling and disabling contracts

It is often the case that contracts are only useful during early phases of development. When the code gets more stable, they can be disabled. Once a problem appear, it may be useful to enable them back just for the time needed to troubleshoot the problem.

The  <xref:Metalama.Patterns.Contracts.ContractOptions> class exposes three properties that allow you to enable or disable contracts for the whole project, or more granularily for a given namespace or type:
* <xref:Metalama.Patterns.Contracts.ContractOptions.ArePreconditionsEnabled>
* <xref:Metalama.Patterns.Contracts.ContractOptions.ArePostconditionsEnabled>
* <xref:Metalama.Patterns.Contracts.ContractOptions.AreInvariantsEnabled>

These options are enabled by default. If you disable them, the code supporting these features will not be generated.

#### Example: disabling invariants in a namespace

In the following example, we have invariants in two sub-namespaces: `Invoicing` and `Fulfillment`. We use a <xref:Metalama.Framework.Fabrics.ProjectFabric> and the <xref:Metalama.Patterns.Contracts.ContractOptions>  to disable invariants the for `Fullfillment` namespace.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/Invariants_Suspend.cs]


### Changing the default inheritance or contract direction options

By default, contract inheritance is enabled and contract direction is set to <xref:Metalama.Framework.Aspects.ContractDirection.Default>. To change these default values, use the <xref:Metalama.Patterns.Contracts.ContractOptions.IsInheritable>   and <xref:Metalama.Patterns.Contracts.ContractOptions.Direction> properties of the <xref:Metalama.Patterns.Contracts.ContractOptions> object.


### Customizing the exception type or text

The default behavior of Metalama Contracts is to generate code that throws the default .NET exception with a hard-coded error message. This default behavior is implemented by the <xref:Metalama.Patterns.Contracts.ContractTemplates> class. 

You may want to throw different kinds of exceptions or use different exception messages, for instance localized in your language. To achieve this, you need to override the <xref:Metalama.Patterns.Contracts.ContractTemplates> class.


To achieve this, follow these steps:

1. Create a class derived from the <xref:Metalama.Patterns.Contracts.ContractTemplates>.
2. Override any or all templates. You may want to look at the original source code of the <xref:Metalama.Patterns.Contracts.ContractTemplates> class to get inspiration.
3. Using a <xref:Metalama.Framework.Fabrics.ProjectFabric> or a <xref:Metalama.Framework.Fabrics.NamespaceFabric>, set the <xref:Metalama.Patterns.Contracts.ContractOptions.Templates> property of the <xref:Metalama.Patterns.Contracts.ContractOptions> object.

#### Example: translating error messages

The following example shows how to translate the exception messages to French. 

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/Localize.cs]