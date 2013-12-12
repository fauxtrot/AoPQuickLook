AoP: At a Glance
============

Source Code for a technical demonstration I'm working on.

The models are the root of where to start for each point to make.  ExampleOne is a vanilla implementation of cross cutting concerns within a method. 

ExampleTwo relies upon IoC and interception provided by Castle Windsor and its dynamic proxy to provide logging within the business method.  It also adds a scenario for throwing an exception.

ExampleThree is the playground for IL Code Weaving via Postsharp.  There is the use case (contrived as it may be) introduced in the previous two models, and several other aspects upon which it relies.

Finally, another aspect is introduced for validation at compile time for the requires serialization attribute.

Multicast targeting for PostSharp is demonstrated with this last item in the AssemblyInfo for the common project.

As I develop this talk, I'll try to introduce more elements. On both sides.

Architectural notes:

* WPF - MVVM using Caliburn.Micro  
* CastleWindsor for IoC  
* PostSharp - IL Code weaver  
* WPF Themes to give it a slightly different look.  

MainView is optimized for being viewed in a projector.

