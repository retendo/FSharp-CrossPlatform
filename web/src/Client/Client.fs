module Client

open Elmish
open Elmish.React

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.PowerPack.Fetch

open Thoth.Json

open NewAppSharedAll
open NewAppSharedFrontend.CounterFrontend

let initialCounter = fetchAs<Counter> "/api/init" (Decode.Auto.generateDecoder())

let mapCommands = function
    | LoadInitialCount -> Cmd.ofPromise initialCounter [] (Ok >> InitialCountLoaded) (Error >> InitialCountLoaded)

let view (model : Model) (dispatch : Msg -> unit) =
    div []
        [ div [] [ str (displayStringFrom model) ]
          button [ OnClick (fun _ -> dispatch Decrement) ] [ str "-" ]
          button [ OnClick (fun _ -> dispatch Increment) ] [ str "+" ] ]

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

module Program =
    /// Typical program, new commands are produced discriminated unions returned by `init` and `update` along with the new state.
    /// From https://github.com/fsprojects/Fabulous/pull/418/commits/3bdf0c2d873f505afbd1941b06d85c8c2b7d8f76#diff-557fd3a3b56fdbf61831e911986bc166
    let mkProgramWithCmdMsg (init: unit -> 'model * 'cmdMsg list) (update: 'msg -> 'model -> 'model * 'cmdMsg list) (view: 'view) (mapToCmd: 'cmdMsg -> Cmd<'msg>) =
        let convert = fun (model, cmdMsgs) -> model, (cmdMsgs |> List.map mapToCmd |> Cmd.batch)
        Program.mkProgram (fun arg -> init arg |> convert) (fun msg model -> update msg model |> convert) view

Program.mkProgramWithCmdMsg init update view mapCommands
#if DEBUG
|> Program.withConsoleTrace
|> Program.withHMR
#endif
|> Program.withReact "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
