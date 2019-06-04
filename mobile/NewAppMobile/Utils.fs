module Utils

let mapErrorToException = function
    | Ok value -> Ok value
    | Error str -> Error (exn str)