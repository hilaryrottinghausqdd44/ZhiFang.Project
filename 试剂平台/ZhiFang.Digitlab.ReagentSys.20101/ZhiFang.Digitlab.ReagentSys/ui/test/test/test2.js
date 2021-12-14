Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var store2 = Ext.create('Ext.data.Store',{
		fields:['name','age','color','checked'],
		data:[
			{name:'asd',age:13,color:'red'},
			{name:'asd',age:13,color:'red'},
			{name:'asd',age:16,color:'red'},
			{name:'sdf',age:16,color:'yellow'},
			{name:'sdf',age:13,color:'yellow'},
			{name:'sdf',age:16,color:'yellow'},
			{name:'kkk',age:13,color:'green'},
			{name:'kkk',age:16,color:'green'}
		]
	});
	
	var store = Ext.create('Ext.data.Store',{
		fields:['name','age','color'],
		data:[{
			name:'aaa',
			age:'13,14,15',
			color:'red'
		},{
			name:'bbb',
			age:'12,13,14',
			color:'green'
		},{
			name:'ccc',
			age:'123,111',
			color:'yellow'
		}]
	});
	
	var panel = Ext.create('Ext.grid.Panel',{
		store:store2,
		title:'拆分列表测试',
		selType:'checkboxmodel',multiSelect:true,
		columnLines:true,
		columns:[{
			//xtype:'rownumberer',text:'序号',width:35,align:'center'
			xtype:'checkcolumn',dataIndex:'checked'
		},{
			text:'姓名',dataIndex:'name'
		},{
			text:'年龄',dataIndex:'age',style:'margin:0',
			renderer:function(value,cellmeta){
                var v = value || "";
                
//                var arr = v.split(",");
//                if(arr.length > 1){
//                	v = [];
//                	for(var i in arr){
//	                	v.push("<p>" + arr[i] + "</p>");
//	                	v.push("<hr width='100%' color=gray size='1'>");
//	                }
//	                //v.push("</div>");
//	                v = v.slice(0,-1).join("");
//                }
                
                return v;
           }
		},{
			text:'颜色',dataIndex:'color',renderer:function(value,cellmeta){
                cellmeta.style = 'background-color:'+value;
                return value;
           }
        }],
        listeners:{
        	viewready:function(g){
        		alert(g.title);
        		mergeCells(panel,[1]);
        	}
        }
		//plugins:[Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1})],//editor:{}
	});
	
	/** 
	* 合并单元格 
	* @param {} grid  要合并单元格的grid对象 
	* @param {} cols  要合并哪几列 [1,2,4] 
	*/  
	function mergeCells(grid, cols) {
	    var arrayTr = document.getElementById(grid.getId()+"-body").firstChild.firstChild.firstChild.getElementsByTagName('tr');  
	    //var arrayTr = document.getElementById(grid.getId()+"-body").firstChild.nextSibling;   
	    //var arrayTr = Ext.get(grid.getId()+"-body").first().first().first().select("tr");   
	    var trCount = arrayTr.length;  
	    var arrayTd;  
	    var td;  
	    var merge = function(rowspanObj, removeObjs) { //定义合并函数   
	        if(rowspanObj.rowspan != 1) {  
	            arrayTd = arrayTr[rowspanObj.tr].getElementsByTagName("td"); //合并行   
	            td = arrayTd[rowspanObj.td - 1];  
	            td.rowSpan = rowspanObj.rowspan;  
	            td.vAlign = "middle";          
	            Ext.each(removeObjs, function(obj) { //隐身被合并的单元格   
	                arrayTd = arrayTr[obj.tr].getElementsByTagName("td");  
	                arrayTd[obj.td - 1].style.display = 'none';  
	            });  
	        }    
	    };  
	    var rowspanObj = {}; //要进行跨列操作的td对象{tr:1,td:2,rowspan:5}   
	    var removeObjs = []; //要进行删除的td对象[{tr:2,td:2},{tr:3,td:2}]   
	    var col;  
	  
	    Ext.each(cols, function(colIndex) { //逐列去操作tr   
	        var rowspan = 1;  
	        var divHtml = null;     //单元格内的数值   
	        for(var i=1; i<trCount; i++) {  //i=0表示表头等没用的行   
	            arrayTd = arrayTr[i].getElementsByTagName("td");  
	            var cold = 0;  
	            Ext.each(arrayTd, function(Td) {    //获取RowNumber列和check列   
	            if(Td.getAttribute("class").indexOf("x-grid-cell-special") != -1)  
	                cold++;                  
	            });  
	            col = colIndex + cold;  //跳过RowNumber列和check列   
	            if(!divHtml) {  
	                divHtml = arrayTd[col-1].innerHTML;  
	                rowspanObj = {tr: i, td: col, rowspan: rowspan}  
	            }else{  
	                var cellText = arrayTd[col-1].innerHTML;  
	                var addf = function() {   
	                  rowspanObj["rowspan"] = rowspanObj["rowspan"] + 1;  
	                  removeObjs.push({tr: i,td: col});  
	                  if(i == trCount-1) merge(rowspanObj, removeObjs);//执行合并函数   
	                };  
	                var mergef = function() {  
	                  merge(rowspanObj, removeObjs);//执行合并函数   
	                  divHtml = cellText;  
	                  rowspanObj = {tr: i, td: col, rowspan: rowspan}  
	                  removeObjs = [];  
	                };  
	                if(cellText == divHtml) {  
	                    if(colIndex != 1) {   
	                    var leftDisplay=arrayTd[col-2].style.display;//判断左边单元格值是否已display   
	                    if(leftDisplay == 'none') addf();    
	                    else mergef();                
	                } else addf();                        
	            } else mergef();        
	          }  
	        }  
	    });  
	};  
	
	var panel1 = Ext.create('Ext.grid.Panel',{
		title:'可编辑列表测试',
		plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}),
		columns:[
			{dataIndex:'id',text:'ID'},
			{dataIndex:'name',text:'NAME',editor:{}},
			{dataIndex:'date',text:'DATE',xtype:'datecolumn',format:'Y-m-d',editor:Ext.create('Ext.zhifangux.DateField',{format:'Y-m-d'})}
		],
		store:Ext.create('Ext.data.Store',{
			fields:['id','name','date'],
			data:[
				{id:'1',name:'张三',date:'2012-12-12'},
				{id:'2',name:'李四',date:'2013-11-12'}
			]
		}),
		dockedItems:[{
			xtype:'toolbar',
			dock:'top',
			items:[{
				text:'修改数据',
				handler:function(but){
					var store = but.ownerCt.ownerCt.store;
					var records = store.getModifiedRecords();
					var obj = {};
					for(var i in records){
						var rec = records[i];
						var date = rec.get('date');
						var name = rec.get('name');
						obj.date = date;
						obj.name = name;
					}
					var a = obj;
				}
			}]
		}]
	});
	
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel1]
	});
});