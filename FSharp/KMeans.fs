namespace Unsupervised

module KMeans =                                 //module用于将名称与一组相关的类型，价值和功能联系起来，在逻辑上它与其他代码中分离出来。
    let pickFrom size k =                       //选择k个观测值作为初始质心,size(int)：数据集大小，k(int)质心个数
        let rng = System.Random()               //定义rng为随机数生成函数，类似于函数引用
        let rec pick (set:int Set) =            //定义一个set变量，是int类型的Set集合，保存所有质心的ID，rec指出该函数是一个递归函数
            let candidate = rng.Next(size)      //生成随机数，随机选取质心并作为ID分配个此观测值，作为初始质心
            let set = set.Add candidate         //把生成的随机数添加到set集合
            if set.Count = k then set           //如果set的大小等于所需要的质心数量，返回set，否则调用自身，继续生成，函数递归的概念
            else pick set
        pick Set.empty |> Set.toArray           //Set.empty创建一个空的集合，pick函数调用结束后，结果通过Set.toArray转换为数组最为pickFrom的返回
        
    let initialize observations k =             //初始化聚类算法
        let size = Array.length observations    //size为observations的长度

        let centroids = 
            pickFrom size k                     //调用pickFrom取到初始质心
            |> Array.mapi (fun i index ->       //通过Array.mapi生成元组
                i+1, observations.[index])      //质心id为0不代表任何实际质心，因此需要加一

        let assignments =                       //观测值生成元组，每个值分配的初始质心都是0
            observations
            |> Array.map (fun x -> 0, x)

        (assignments, centroids)                //返回的元组，（标记为0的观测值,质心id集合)

    let clusterize distance centroidOf observations k =         //递归进行聚类更新
        
        let rec search (assignments, centroids) = 
            //最近的质心ID
            let classifier observation =                         //分类器
                centroids                                           
                |> Array.minBy (fun (_,centroid) ->              //找出当前观测值在给出的距离函数下，最近的距离，注意，可能会有多个
                    distance observation centroid)
                |> fst                                           //当出现距离相等时，取第一个值
            //为每一组观测值标记最近的质心
            let assignments' = 
                assignments
                |> Array.map (fun (_,observation) ->                    //生成元组，经过分类器标记的观测值
                    let closestCentroidId = classifier observation      
                    (closestCentroidId,observation))                    //返回的元组类型，(质心id,观测值)
            //检查是否有观测值改变了所属聚类
            let changed = 
                (assignments, assignments')
                ||> Seq.zip
                |> Seq.exists (fun ((oldClusterID,_),(newClusterID,_)) ->
                    not (oldClusterID = newClusterID))

            //调用changed函数，如果返回值为true，继续更新
            if changed
            then
                let centroids' = 
                    assignments'
                    |> Seq.groupBy fst
                    |> Seq.map (fun (clusterID, group) -> 
                        clusterID, group |> Seq.map snd |> centroidOf)
                    |> Seq.toArray
                search (assignments', centroids')
            else centroids,classifier

        //从初始值开始更新
        let initialValues = initialize observations k
        search initialValues
                