namespace Samples

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Html
open WebSharper.UI.Client
open WebSharper.IntroJS

[<Require(typeof<WebSharper.IntroJS.Resources.MainTheme>)>]
[<JavaScript>]
module HelloWorld =
    
    type IndexTemplate = Templating.Template<"wwwroot/index.html">

    let People =
        ListModel.FromSeq [
            "John"
            "Paul"
        ]

    [<SPAEntryPoint>]
    let Main () =
        let newName = Var.Create ""

        IndexTemplate.Main()
            .ListContainer(
                People.View.DocSeqCached(fun (name: string) ->
                    IndexTemplate.ListItem().Name(name).Doc()
                )
            )
            .Name(newName)
            .Add(fun _ ->
                People.Add(newName.Value)
                newName.Value <- ""
            )
            .Intro(fun _ ->
                IntroJS.IntroJs().Start() |> ignore
            )
            .Doc()
        |> Doc.RunById "main"
