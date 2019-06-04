namespace NewAppSharedFrontend

open NewAppSharedAll

module CounterFrontend =
    type Model = { Counter: Counter option }

    type Msg = 
        | Increment 
        | Decrement 
        | InitialCountLoaded of Result<Counter, exn>

    type CmdMsg =
        | LoadInitialCount

    let init () = { Counter = None }, [ LoadInitialCount ]

    let update msg model : Model * CmdMsg list =
       match model.Counter, msg with
       | Some counter, Increment ->
           { model with Counter = Some { Value = counter.Value + 1 } }, []
       | Some counter, Decrement ->
           { model with Counter = Some { Value = counter.Value - 1 } }, []
       | _, InitialCountLoaded (Ok initialCount) ->
           { Counter = Some initialCount }, []
       | _ -> model, []

    let displayStringFrom = function
        | { Counter = Some counter } -> string counter.Value
        | { Counter = None   } -> "Loading..."
