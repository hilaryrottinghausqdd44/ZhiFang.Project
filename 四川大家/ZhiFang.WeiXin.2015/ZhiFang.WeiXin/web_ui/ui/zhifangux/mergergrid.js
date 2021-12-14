/**
 * 定制的合并列表
 * 【必配参数】
 * store 数据集合
 * mergedMainColumn 合并的主列
 * 【可配参数】
 * mergedOtherColumns 需要合并的列
 * bgcolorColumns 背景色列
 * 【Example】
 * 
 * isNumLine:是否是分组数字列
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.mergergrid',{
	extend:'Ext.grid.Panel',
	mainValue:'mainValue',
	isNumLine:'isNumLine',
	isCheckbox:'isCheckbox',
    mergedMainColumn:'',
    mergedOtherColumns:[],
    bgcolorColumns:[],
    /**默认hql*/
    defaultWhere:'',
    /**内部hql*/
    internalWhere:'',
    /**外部hql*/
    externalWhere:'',
    searchUrl:'',
	initComponent:function(){
		var me = this;
		//列配置
		me.changeColumns();
		//处理数据集合
		me.changeStoreData(me.store);
		me.callParent(arguments);
	},
	/**
	 * 更改列配置
	 * @private
	 */
	changeColumns:function(){
		var me = this;
		var bgcolorColumns = me.bgcolorColumns;
		//更改背景色列
		var change = function(field){
			for(var i=0;i<me.columns.length;i++){
				var dataIndex = me.columns[i].dataIndex;
				if(field == dataIndex){
					me.columns[i].renderer = function(v,m){
            			m.style='background:'+v+';';
                		return "";  
            		}
				}
			}
		}
		
		if(bgcolorColumns && bgcolorColumns.length > 0){
			for(var i in bgcolorColumns){
				change(bgcolorColumns[i]);
			}
		}
	},

	/**
	 * 获取需要合并的列的下标
	 * @private
	 * @return {}
	 */
	getColsIndex:function(){
		var me = this;
		var fields = me.mergedOtherColumns;
		
		var indexArr = [];
		for(var i in fields){
			//名称对应的下标
			var index = me.getIndexByIndexData(fields[i]);
			if(index != -1){
				indexArr.push(index);
			}
		}
		return indexArr;
	},
	/**
	 * 获取列名称对应的下标
	 * @private
	 * @param {} field
	 * @return {}
	 */
	getIndexByIndexData:function(field){
		var me = this;
		for(var i=0;i<me.columns.length;i++){
			var dataIndex = me.columns[i].dataIndex;
			if(field == dataIndex){
				return i+1;
			}
		}
		return -1;
	},
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == "function") {
            me.callback(me);
        }
    },
	/**
	 * 处理数据集合
	 * @private
	 */
	changeStoreData:function(){
		var me = this;
		var proxy = me.store.proxy;
		var root = me.store.proxy.reader.root;
		var totalProperty = me.store.proxy.reader.totalProperty;
		var data = proxy.data;
		if(data){//数据已经存在
			var v = me.getChangedData(data);
			me.store.loadData(v);
			if(!me.listeners){me.listeners = {};}
			//合并单元格
			me.listeners.viewready = function(com,eOpts){me.mergeCells();};
		}else{//数据需要从服务中获取
			me.store.proxy.extractResponseData = function(response){
				var result = Ext.JSON.decode(response.responseText);
		    	if(root && root != ""){
                    //有问题
                    var tempLists=result[root];
                    if(tempLists==undefined){
                        var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
			            result.count = ResultDataValue.count;
			            tempLists = ResultDataValue.list;
                    }
		    		result[root] = me.getChangedData(tempLists);
		    	}else{
		    		result = me.getChangedData(result);
		    	}
				
				response.responseText = Ext.JSON.encode(result);
		    	return response;
			};
			if(!me.store.listeners){me.store.listeners = {};}
				me.store.listeners.load = function(store,records,successful,eOpts){
					me.mergeCells();//合并单元格
	                if(me.selType == "checkboxmodel"){//复选框
			            //me.getSelectionModel().selectAll();
			        }
				}
			}
	},
	/**
	 * 进行数据匹配，用于合并的数据处理
	 * @private
	 * @param {} list
	 * @return {}
	 */
	getChangedData:function(lists){
		var me = this;
		var v = lists;
		var num = 1;
		var insertArr = [];
        if(lists&&lists!=undefined){
			if(lists.length > 0){
				lists[0][me.mainValue] = lists[0][me.mergedMainColumn];
				lists[0][me.isNumLine] = false;
				insertArr.push({key:lists[0][me.mergedMainColumn],value:num,index:0});
			}
			for(var i=1;i<lists.length;i++){
				lists[i][me.mainValue] = lists[i][me.mergedMainColumn];
				lists[i][me.isNumLine] = false;
				var newData = lists[i][me.mergedMainColumn];
				var oldData = lists[i-1][me.mergedMainColumn];
				if(newData != oldData){//需要添加一行
                    
					insertArr.push({key:newData,value:++num,index:i});
				}
			}
			
			for(var i=0;i<insertArr.length;i++){
				var index = insertArr[i].index;
				var obj = {};
				obj[me.mergedMainColumn] = insertArr[i].value;
				obj[me.mainValue] = insertArr[i].key;
				obj[me.isNumLine] = true;
				v.splice(index+i,0,obj);
			}
        }else{
            v=[];
        }
		return v;
	},
	/**
	 * 合并的主列键值标记
	 * @private
	 */
	insertMainMark:function(){
		var me = this;
		var arrayTr=document.getElementById(me.getId()+"-body").firstChild.firstChild.firstChild.getElementsByTagName('tr');
		var mianIndex = me.getMainIndex();//合并的主列的下标
		for(var i=1;i<arrayTr.length;i++){
			var tds = arrayTr[i].getElementsByTagName("td");
			tds[mianIndex-1][me.mainValue] = me.store.proxy.data[i-1][me.mainValue];
			tds[mianIndex-1][me.isNumLine] = me.store.proxy.data[i-1][me.isNumLine];
			tds[mianIndex-1][me.isCheckbox] = false;
			
			if(me.selType == "checkboxmodel"){//复选框
				tds[0][me.isCheckbox] = true;
			}
			
			if(tds[mianIndex-1][me.isNumLine]){
				tds[mianIndex-1].colSpan = me.columns.length;
			}
		}
	},
	/** 
	 * 合并单元格
	 * @private
	 */    
	mergeCells:function(){
		var me = this;
		me.insertMainMark();
		var cols = me.getColsIndex();//需要合并的列
		if(me.selType == "checkboxmodel"){//复选框
			cols.splice(0,0,1);
		}
		var mianIndex = me.getMainIndex();//合并的主列的下标
		
	    var arrayTr=document.getElementById(me.getId()+"-body").firstChild.firstChild.firstChild.getElementsByTagName('tr');     
	    var trCount = arrayTr.length;
	    var arrayTd;
	    var td;
	    var merge = function(rowspanObj,removeObjs){ //定义合并函数
	    	if(rowspanObj.rowspan != 1){
	    		arrayTd =arrayTr[rowspanObj.tr].getElementsByTagName("td");//合并行
	            td=arrayTd[rowspanObj.td-1];
	            td.rowSpan=rowspanObj.rowspan;
	            td.vAlign="middle";
	            //=================填充背景色的高度=====================
	            var v = Ext.String.trim(td.children[0].innerHTML);
	            if(v == "" || v == "&nbsp;"){
	            	for(var i=0;i<rowspanObj.rowspan;i++){
	            		td.children[0].innerHTML += "<br>&nbsp;";
	            	}
	            }
	            //=====================================================
	            Ext.each(removeObjs,function(obj){//隐身被合并的单元格
	                arrayTd =arrayTr[obj.tr].getElementsByTagName("td");
	                arrayTd[obj.td-1].style.display='none';
	            });
	        }
	    };
	    var rowspanObj = {};//要进行跨列操作的td对象{tr:1,td:2,rowspan:5}
	    var removeObjs = []; //要进行删除的td对象[{tr:2,td:2},{tr:3,td:2}]
	    var col;
	    Ext.each(cols,function(colIndex){//逐列去操作tr
	        var rowspan = 1;
	        var divHtml = null;//单元格内的数值
	        for(var i=1;i<trCount;i++){//i=0表示表头等没用的行
	            arrayTd = arrayTr[i].getElementsByTagName("td");
	            var cold=0;    
//	          	Ext.each(arrayTd,function(Td){//获取RowNumber列和check 列
//	              	if(Td.getAttribute("class").indexOf("x-grid-cell-special") != -1)
//	                  	cold++;
//	          	});
	            col=colIndex+cold;//跳过RowNumber列和check列
	            if(!divHtml){
	                divHtml = arrayTd[mianIndex-1][me.mainValue];
	                rowspanObj = {tr:i,td:col,rowspan:rowspan}
	            }else{
	                var cellText = arrayTd[mianIndex-1][me.mainValue];
	                var addf=function(){
	                    rowspanObj["rowspan"] = rowspanObj["rowspan"]+1;
	                    removeObjs.push({tr:i,td:col});
	                    if(i==trCount-1)
	                        merge(rowspanObj,removeObjs);//执行合并函数
	                };
	                var mergef=function(){
	                    merge(rowspanObj,removeObjs);//执行合并函数
	                    divHtml = cellText;
	                    rowspanObj = {tr:i,td:col,rowspan:rowspan}
	                    removeObjs = [];
	                };
	                var topArrayTd = arrayTr[i-1].getElementsByTagName("td");
	                if(cellText == divHtml){
                    	if(topArrayTd[mianIndex-1][me.mainValue] == arrayTd[mianIndex-1][me.mainValue]){
                    		if(topArrayTd[col-1][me.isCheckbox]){//复选框
                    			addf();
                    		}else{
                    			if(topArrayTd[mianIndex-1][me.isNumLine]){
                    				mergef();
                    			}else{
                    				addf();
                    			}
                    		}
                    	}else{
                			mergef();
                		}
	                }else{
	                    mergef();
	                }
	            }
	        }
	    });
	},
	/**
	 * 获取合并的主列下标
	 * @private
	 * @return {}
	 */
	getMainIndex:function(){
		var me = this;
		//合并的主列的下标
		var mianIndex = me.getIndexByIndexData(me.mergedMainColumn);
		return mianIndex;
	}
});