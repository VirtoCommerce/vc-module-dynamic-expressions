# VirtoCommerce.DynamicExpressions

## Overview

VirtoCommerce.DynamicExpressions module enables building complex dynamic expressions while using user-friendly management UI. Includes expressions for marketing (promotions and content publishing) and pricing dynamic condition builders.

Sometimes there is a need to give an end-user a way of composing conditions under which target objects should be filtered and returned to the client. For example, "the product should be available to the shopper only if he is more than 25 years old". Simplifying condition expressions building for the end-user is often a challenge.

The most common way to build condition expressions is by using a visual UI. That way the user builds the condition expression as a regular sentence. While reading a regular sentence it is easy to understand the specific circumstances the result will yield: object returned. 

Key features:

* extendible expression list (create and use custom expressions from your own module).

![Dynamic expression management UI](https://cloud.githubusercontent.com/assets/5801549/15645509/936e75aa-2661-11e6-9b73-6786905e4fa6.png)

[Add Dynamic Expressions to Marketing Dynamic Content](/docs/add-expressions-to-dynamic-content.md)

[Add Dynamic Expressions To Promotions](/docs/add-dynamic-expression-to-promotions.md)

[Compose Dynamic Conditions](/docs/compose-dynamic-conditions.md)

# Documentation
User guide:
* add dynamic expressions to marketing <a href="https://virtocommerce.com/docs/vc2userguide/marketing/dynamic-content" target="_blank">Dynamic content</a>
* add dynamic expressions to marketing <a href="https://virtocommerce.com/docs/vc2userguide/marketing/promotions" target="_blank">Promotions</a>

Developer guide: <a href="https://virtocommerce.com/docs/vc2devguide/working-with-platform-manager/extending-functionality/composing-dynamic-conditions" target="_blank">composing dynamic conditions</a>


# Installation
Installing the module:
* Automatically: in VC Manager go to Configuration -> Modules -> Virto Commerce dynamic expression library module -> Install
* Manually: download module zip package from https://github.com/VirtoCommerce/vc-module-dynamic-expressions/releases. In VC Manager go to Configuration -> Modules -> Advanced -> upload module package -> Install.


# Available resources
* Module related service implementations as a <a href="https://www.nuget.org/packages/VirtoCommerce.DynamicExpressionsModule.Data" target="_blank">NuGet package</a>


# License
Copyright (c) Virto Solutions LTD.  All rights reserved.

Licensed under the Virto Commerce Open Software License (the "License"); you
may not use this file except in compliance with the License. You may
obtain a copy of the License at

http://virtocommerce.com/opensourcelicense

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
implied.
