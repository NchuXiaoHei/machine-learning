open System.IO

type Observation = {Label: string; Pixels: int[]}

type Distance = int[] * int[] -> int        //它是一个类型，这里作用是作为一个函数签名，表示该函数接收两个int[]参数
                                            //并返回一个int

let toObservation (csvData: string) = 
    let columns = csvData.Split(',')
    let label = columns.[0]
    let pixels = columns.[1..] |> Array.map int
    {Label = label; Pixels = pixels}

let reader (path:string) = 
    let data = File.ReadAllLines path
    data.[1..]
    |> Array.map toObservation                  //Array.map 将数据映射，这里标识使用toObservation去映射数据，得到一个
                                                //toObservation返回值的数据数组，也就是 {Label, Pixels} 形式的数据的数组

let trainingPath = @"D:\SojS\machine-learning\machine-learning-projects-for-dot-net-developers\chapter-1\DigitsRecognizer\Data\trainingsample.csv"
let trainingData = reader trainingPath

let manhattanDistance (pixels1,pixels2) = 
    Array.zip pixels1 pixels2                           //Array.zip 将数据打包为 元组，相同索引的元素被配对在一起，可以利用模式匹配操作元组
    |> Array.map (fun (x,y) -> abs (x-y))               //映射
    |> Array.sum

let euclideanDistance (pixels1, pixels2) = 
    Array.zip pixels1 pixels2
    |> Array.map (fun (x,y) -> pown (x-y) 2)
    |> Array.sum

let train (trainingset:Observation[]) (dist: Distance) = 
    let classify (pixels:int[]) = 
        trainingset
        |> Array.minBy (fun x -> dist (x.Pixels,pixels))
        |> fun x -> x.Label
    classify

let classifier = train trainingData

let manhattanClassifier = train trainingData manhattanDistance
let euclideanClassifier = train trainingData euclideanDistance

let validationPath = @"D:\SojS\machine-learning\machine-learning-projects-for-dot-net-developers\chapter-1\DigitsRecognizer\Data\validationsample.csv"
let validationData = reader validationPath

let evaluate validationSet classifier = 
    validationSet
    |> Array.averageBy (fun x -> if classifier x.Pixels = x.Label then 1. else 0.)
    |> printfn "Correct: %.3f"

printfn "Manhattan"
evaluate validationData manhattanClassifier
printfn "Euclidean"
evaluate validationData euclideanClassifier