Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	Ext.Loader.setPath('Ext.zhifangux', getRootPath()+'/ui/zhifangux');
	//数据集
	var data = [
		{f1:'1001',f2:'肾功',f3:'red',f4:'生化',f5:'全血',f6:'AAAAAA'},
		{f1:'1001',f2:'血脂分析',f3:'red',f4:'生化',f5:'全血',f6:'AAAAAA'},
		{f1:'1002',f2:'尿沉渣检查',f3:'yellow',f4:'临检',f5:'全血',f6:'AAAAAA'},
		{f1:'1003',f2:'两对半(乙肝五项)',f3:'blue',f4:'免疫',f5:'全血',f6:'BBBBBB'}
	];
	//合并的主列
	var mergedMainColumn = 'f1';
	//需要合并的列
	var mergedOtherColumns = ['f1','f3','f4','f5'];
	//背景色的列
	var bgcolorColumns = ['f3'];
	//数据集
	var store = Ext.create('Ext.data.Store',{
		fields:['f1','f2','f3','f4','f5','f6'],
		data:data
//		proxy:{
//            type:'ajax',
//            url:'grid001.json',
//            reader:{
//            	type:'json',
//            	totalProperty:'count',
//                root:'list'
//            }
//        }
	});
	var grid = Ext.create('Ext.zhifangux.mergergrid',{
		//--------------必配参数---------------
		mergedMainColumn:mergedMainColumn,
		mergedOtherColumns:mergedOtherColumns,
		bgcolorColumns:bgcolorColumns,
		store:store,
		//------------------------------------
		title:'列表测试',
		width:600,
		height:400,
		
		columnLines:true,
		rowLines:true,
		sortableColumns:false,
		selType:'checkboxmodel',//复选框
		multiSelect:true,//允许多选
		columns:[
			{dataIndex:'f1',text:'F1'},//editor:{allowBlank:false}},
			{dataIndex:'f2',text:'F2'},
			{dataIndex:'f3',text:'',width:10},
			{dataIndex:'f4',text:'F4'},
			{dataIndex:'f5',text:'F5'},
			{dataIndex:'f6',text:'F6'}
		],
		plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}),
		dockedItems:[{
			xtype:'toolbar',
			items:[{
				text:'获取选中的数据',
				handler:function(but){
					var me = this.ownerCt.ownerCt;
					var records = me.getSelectionModel().getSelection();
					var str = "";
					for(var i in records){
						str = str + records[i].get('f1') + ",";
					}
					if(str.length > 0){
						str = str.substring(0,str.length-1);
					}
					alert(str);
				}
			},{
				text:'选中',
				handler:function(but){
					var me = this.ownerCt.ownerCt;
					var selection = me.getSelectionModel()
					//selection.select([2,3,4],true);
					//selection.selectAll();
					
					var a = me.store.query('f1','1001');
					var c = 1;
				}
			}]
		}],
		listeners:{
			select:function(row,record,index){
				var a = 1;
			}
		}
	});
	
	Ext.create('Ext.container.Viewport',{
		padding:2,
		items:[grid]
	});
});