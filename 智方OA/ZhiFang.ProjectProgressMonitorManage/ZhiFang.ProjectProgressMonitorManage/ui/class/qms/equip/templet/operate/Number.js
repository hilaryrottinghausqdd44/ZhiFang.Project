/**
 * 重写数字框
 * @author liangyl
 * @version 2017-12-12
 */
Ext.define('Shell.class.qms.equip.templet.operate.Number', {
   extend: 'Shell.ux.form.Panel',
    alias: 'widget.uxnumberfield',
    title: '重写数字框组件',
	height: 30,
	bodyPadding: '0px 0px 0px 10px;',
	formtype:'add',
	header:false,
	border:false,
	maxValue:1000000000,
	minValue:-9999999990,
		bodyStyle :'overflow-x:hidden;overflow-y:scroll', 
     /**布局方式*/
	layout: 'anchor',
	/**这里允许保留位小数*/
	DecimalLength:3,	
	/**是否允许小数点*/
	allowDecimals:true,
	DefaultValue:null,
	/**上次数据*/
	preValue:null,
	/**步距*/
	AddValue:'2',
    /**项目代码*/
	ItemCode:'C',
	/**模板ID*/
	TempletID:null,
	/**操作日期*/
	OperateDate:null,
	bodyStyle: {background: '#ffffff'},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	   
	},
	initComponent: function() {
		var me = this;
		me.title = me.title || "重写数字框";
		if(me.AddValue)me.height=65;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Number = Ext.create('Ext.form.field.Number', {
			fieldLabel: '', header:false,
			height:25,
			value: me.DefaultValue,
			step: 1,itemId: 'Number',
            region:'center'	, width:me.width-10,
			name: 'Number'
		});
		if( me.maxValue){
			me.Number.maxValue=me.maxValue;
		}
		if( me.minValue){
			me.Number.minValue=me.minValue;
		}
		if( me.allowDecimals){
			me.Number.allowDecimals=me.allowDecimals;
		}
		if(me.DecimalLength){
			me.Number.decimalPrecision=me.DecimalLength;
		}
		me.PrePanel = Ext.create('Ext.form.Label', {
	        width:100,
	        labelWidth:60,
	        height:25,
	        itemId: 'PrePanel',
	        margin:'10px 0px 10px 0px;',
	        text: '上次数据',
	        name: 'PrePanel'
		});
		me.StepPanel = Ext.create('Ext.form.Label', {
	        width:60,    labelWidth:35,
	        itemId: 'StepPanel',
	        text: '步距:'+me.AddValue,
	        height:25,
	        margin:'10px 0px 10px 0px;',
	        name: 'StepPanel'
		});
		me.myPanel = Ext.create('Ext.panel.Panel', {
            header:false,border:false,	        
	        text: '',height:35,width:me.width-10,
	        region:'north',
	        layout: {
				type: 'hbox',
			    pack: 'start',
			    align: 'stretch'
			},
	        items:[me.PrePanel,me.StepPanel]
		});
		var items=[ me.Number];
		if(me.AddValue){
			items.splice(0,0,me.myPanel);
		}
		return items;
	},
	/**
	 * 设置布局，上次数据和今天数据
	 * */
	setLoadData:function(){
		var me=this;
		var PrePanel=me.myPanel.getComponent('PrePanel');
		var pre,nowval=null;
		if(!me.AddValue) return;
		me.getDataById(me.TempletID,function(data){
			//从来没有填写过数据
			if(!data || !data.value){
			   	pre=null;
            	nowval=me.AddValue;
            }else{//多条数据
            	var len=data.value.list.length;
//          	var Sysdate=JcallShell.System.Date.getDate();
//	            var operatedate = JcallShell.Date.toString(Sysdate, true);
                var operatedate =  me.OperateDate;
	            //取到时间
	            var DataAddTime=data.value.list[0].EMaintenanceData_ItemDate;
            	if(DataAddTime) var AddTime=JcallShell.Date.toString(DataAddTime, true);
            	//第一行是今天
            	if(AddTime==operatedate){
            		//只有一条数据时,上次数据没设置，默认值为null（空）
            		pre= len ==1 ? null : data.value.list[1].EMaintenanceData_ItemResult;
            		nowval = data.value.list[0].EMaintenanceData_ItemResult;
            	}else{
            		pre=data.value.list[0].EMaintenanceData_ItemResult;
            		nowval= Number(data.value.list[0].EMaintenanceData_ItemResult)+ Number(me.AddValue);
            	}
            }
            if(pre==null) pre='空';
			PrePanel.setText('上次数据:'+pre);
			me.Number.setValue(nowval);
		});
	},
	getValue:function(){
		var me=this;
		var value=me.Number.getValue();
		return value;
	},
	setValue:function(value){
		var me=this;
		me.Number.setValue(value);
	},
	/**
	 * 获取该模板的最后两条数据
	 * id 模板ID
	 * [DataAddTime]  时间
	 * [ItemCode] 项目代码
	 * */
	getDataById:function(id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/QMSReport.svc/ST_UDTO_SearchEMaintenanceDataByHQL?isPlanish=true';
		url += '&fields=EMaintenanceData_ItemDate,EMaintenanceData_ItemResult';
		var Sysdate=JcallShell.System.Date.getDate();
	    var ndate=JShell.Date.toString(JShell.Date.getNextDate(me.OperateDate),true);	    	
	    if(!id || !ndate || !me.ItemCode ) return;
	    var  where='emaintenancedata.ETemplet.Id='+id;
	    where +=" and emaintenancedata.TempletItemCode='"+ me.ItemCode+"'" +" and emaintenancedata.ItemDate<'"+ndate+"'";
		url+='&where='+where;
		url+='&sort=[{"property":"EMaintenanceData_ItemDate","direction":"DESC"}]';
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick:function(){
		var me = this;
		if(!me.PK){
			this.getForm().reset();
			me.setLoadData();
		}else{
			me.getForm().setValues(me.lastData);
		}
	}
	
});