/**
 * 字典信息拷贝
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.dict.InfoCopyPanel',{
	extend:'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'字典信息拷贝',
    width:600,
	height:300,
	bodyPadding:10,
	
	formtype:'add',
	/**每个组件的默认属性*/
    defaults:{
    	width:200,
        labelWidth:30,
        labelAlign:'right'
    },
    /**是否启用保存按钮*/
	hasSave:false,
	/**是否退出按钮*/
	hasCancel:true,
	
	/**需要拷贝的数据*/
	InfoRecords:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(!me.hasSave){
			me.initListeners();
			me.changeContent();
		}
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
			
		items = [{
			x:10,y:10,labelWidth:40,width:140,
			fieldLabel:'分隔符',xtype:'uxSimpleComboBox',
			itemId:'separator',value:'	',
			data:[['	','Excel制表符'],['|','|']]
		},{
			x:150,y:10,labelWidth:30,width:150,
			fieldLabel:'类型',xtype:'uxSimpleComboBox',
			itemId:'type',value:'1',
			data:[
				['1','名称'],['2','名称+编号'],['3','名称+编号+备注']
			]
		},{
			x:10,y:40,width:570,
			fieldLabel:'内容',height:180,xtype:'textarea',
			itemId:'content',resizable:true,resizeHandles:'se'
		}];
		
		return items;
	},
	initListeners:function(){
		var me = this,
			separator = me.getComponent('separator'),
			type = me.getComponent('type');
			
		separator.on({
			change:function(field,newV,oldV){
				me.changeContent();
			}
		});
		type.on({
			change:function(field,newV,oldV){
				me.changeContent();
			}
		});
	},
	changeContent:function(){
		var me = this,
			content = me.getComponent('content'),
			list = [];
		
		if(!me.InfoRecords) return;
		
		var len = me.InfoRecords.length;
		for(var i=0;i<len;i++){
			list.push(me.getOneInfo(me.InfoRecords[i]));
		}
		
		content.setValue(list.join('\r\n'));
	},
	getOneInfo:function(record){
		var me = this,
			separator = me.getComponent('separator'),
			type = me.getComponent('type'),
			separatorV = separator.getValue(),
			typeV = type.getValue(),
			info = '';
		
		if(typeV == '1'){
			info = record.get('BDict_CName')
		}else if(typeV == '2'){
			info = record.get('BDict_CName') + 
				separatorV + record.get('BDict_Shortcode');
		}else if(typeV == '3'){
			info = record.get('BDict_CName') + 
				separatorV + record.get('BDict_Shortcode') + 
				separatorV + record.get('BDict_Memo');
		}
		
		return info;
	},
	isAdd:function(){},
	onSaveClick:function(){
		var me = this,
			content = me.getComponent('content'),
			contentV = content.getValue(),
			list = [];
		
		if(!JShell.String.trim(contentV)) return;
		
		contentV = contentV.replace(/[\r\n]/g, '\\r\\n');
		var arr = contentV.split('\\r\\n');
		var len = arr.length;
		for(var i=0;i<len;i++){
			var data = me.getOneRecord(arr[i]);
			if(data){
				list.push(data);
			}
		}
		
		if(list.length > 0){
			me.fireEvent('save',me,list);
		}
	},
	getOneRecord:function(info){
		var me = this,
			separator = me.getComponent('separator'),
			type = me.getComponent('type'),
			separatorV = separator.getValue(),
			typeV = type.getValue(),
			data = {};
			
		if(!JShell.String.trim(info)) return null;
		
		var arr = info.split(separatorV);
		if(typeV == '1'){
			data.BDict_CName = arr[0];
		}else if(typeV == '2'){
			data.BDict_CName = arr[0];
			data.BDict_Shortcode = arr[1];
		}else if(typeV == '3'){
			data.BDict_CName = arr[0];
			data.BDict_Shortcode = arr[1];
			data.BDict_Memo = arr[2];
		}
		
		return data;
	}
});