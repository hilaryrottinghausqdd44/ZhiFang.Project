//判断是否环链
var isLinkCycle = function(node){
	//已遍历节点数组
	var arr = [];
	//节点是否已存在
	var hasEqualObj = function(n){
		for(var i in arr){
			if(n.equal(arr[i])){
				return true;
			}
		}
		return false;
	};
	//节点处理
	var nodeJudgment = function(n){
		//节点是否已存在
		var bo = hasEqualObj(n);
		//存放该节点
		arr.push(n);
		if(bo){
			return true;
		}else{
			if(n.nextNode){
				return nodeJudgment(n.nextNode);
			}else{
				return false;
			}
		}
	};
	var isCycle = nodeJudgment(node);
	return isCycle;
};