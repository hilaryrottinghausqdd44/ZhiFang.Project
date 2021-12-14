/**
 * 未选项目面板
 * @author liangyl
 * @version 2019-12-23
 */
Ext.define('Shell.class.lts.batchedit.item.un.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'未选项目',
    
    //小组ID
	SectionID: null,
    /**项目类型,0-全部项目,1-医嘱项目，2-组合项目*/
	type:'0' ,
	 /**获取组合项目子项服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true',

    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	    me.ChildGrid.hide();
	    me.UnGrid.on({
			itemdblclick: function (v, record) {
				if (record.get("LBSectionItem_LBItem_GroupType") == '1') {//组合项目
					me.fireEvent("itemclickAddLeft", v, me.getSelect());
				} else {
					me.fireEvent("itemclickAddLeft", v, record.data);
				}
			},
			itemclick: function (v, record) {
				me.onSearch(record);
			},
			select:function(RowModel, record){
				me.onSearch(record);
			},
			nodata:function(){
				me.ChildGrid.clearData();
			}
	    });
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.UnGrid = Ext.create('Shell.class.lts.batchedit.item.un.UnGrid', {
			region:'center',
			itemId:'Info',
			SectionID:me.SectionID,
			header:false,
			type:me.type
		});
		me.ChildGrid = Ext.create('Shell.class.lts.batchedit.item.un.ChildGrid', {
			region:'east',
			width:400,
			itemId:'ChildGrid',
			split:true,
			collapsible:false,
			title:'组合项目子项',
			header:true
		});
		return [me.UnGrid,me.ChildGrid];
	},
	onSearch:function(record){
		var me = this;
		JShell.Action.delay(function(){
			var GroupType = record.get('LBSectionItem_LBItem_GroupType');
	        //GroupType=1 ,组合项目,不是组合项目子项目列表隐藏
	        if(GroupType=='1'){
	        	me.ChildGrid.show();
				me.ChildGrid.loadDataByID(record.get('LBSectionItem_LBItem_Id'));
	        }else{
	        	me.ChildGrid.hide();
	        }
        },null,200);
	},
	//获取选择的值
	getSelect: function(){
		var me = this;
		var records = me.UnGrid.getSelectionModel().getSelection();
		var arr =[],list=[];
		for(var i=0;i<records.length;i++){
			//组合项目,循环子项
			if(records[i].data.LBSectionItem_LBItem_GroupType == '1'){
				me.getItemGroup(records[i].data.LBSectionItem_LBItem_Id,function(list){
					for(var j=0;j<list.length;j++){
						list[j].LBSectionItem_LBItem_GroupItemCName = records[i].data.LBSectionItem_LBItem_CName;
						list[j].LBSectionItem_LBItem_GroupItemID = records[i].data.LBSectionItem_LBItem_Id;
						list[j].LBSectionItem_LBItem_GroupItemUseCode = records[i].data.LBSectionItem_LBItem_UseCode;
					   	list[j].LBSectionItem_LBItem_GroupType = records[i].data.LBSectionItem_LBItem_GroupType;
					    var result = Ext.encode(list[j]);
						var obj = result.replace(/LBItemGroup/g,"LBSectionItem");
						obj = Ext.JSON.decode(obj);
						var isExist = me.isExist(arr,obj.LBSectionItem_LBItem_Id);
						if(!isExist)arr.push(obj);
					}
				});
			}else{
				var isExist = me.isExist(arr,records[i].data.LBSectionItem_LBItem_Id);
				if(!isExist)arr.push(records[i].data);
			}
		}
		return arr;
	},
	//判断对象是否已存在数组中
	isExist : function(arr,id){
		var me = this,
		    isExist=false;
		for (var i = 0; i < arr.length; i++) {
            if ((arr[i].LBSectionItem_LBItem_Id).indexOf(id) > -1) {//判断key为id的对象是否存在，
	              isExist = true;
	              break;
            }
        }
		return isExist;
	},
	/**或者组合项目子项*/
	getItemGroup: function(GroupItemID,callback) {
		var me = this,
			url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
			
		url += '&fields=' + me.ChildGrid.getStoreFields(true).join(',');
		url += '&where=GroupItemID='+GroupItemID;
		url += '&sort=[{"property":"LBItemGroup_LBItem_DispOrder","direction":"ASC"}]';
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				var list = data.value ? data.value.list : [];
				callback(list);
			} 
		},false);
	}
});