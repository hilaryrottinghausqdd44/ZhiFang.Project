/**医嘱单号,消费单号,大家价格,折扣价格 列隐藏显示
 * @author liangyl	
 * @version 2017-02-23
 */
Ext.define('Shell.class.weixin.ordersys.seach.ColForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'自定义列配置',
    width:280,
    height:150,
    bodyPadding:10,

    /**新增服务地址*/
    addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPContract',
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractByField',

    /**是否启用保存按钮*/
    hasSave:true,
    /**是否重置按钮*/
    hasReset:false,
    
    /**布局方式*/
	layout: 'anchor',
	formtype:'edit',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 5,
		labelAlign: 'right'
	},
	
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        var myCheckboxGroup=me.getComponent('myCheckboxGroup');
        var Arr=[];
	    if(me.LocalStorage.get('OSUserOrderFormGrid')) {
	    	var tempArr = Ext.JSON.decode(me.LocalStorage.get('OSUserOrderFormGrid'));
	    	if(tempArr)Arr=Arr.concat(tempArr);
	    	for (var i = 0; i < myCheckboxGroup.items.length; i++)    
            {     
            	myCheckboxGroup.items.items[i].setValue(false);
            	for(var j = 0; j < Arr.length; j++) {
            		if(Arr[j] == myCheckboxGroup.items.items[i].inputValue) {
            		    myCheckboxGroup.items.items[i].setValue(true);
            		    break;
            		}
            	}
            }    
		}
    },
    initComponent: function () {
        var me = this;
        me.callParent(arguments);
   },
    /**@overwrite 创建内部组件*/
    createItems: function () {
        var me = this;
		var items = [{
	        xtype: 'checkboxgroup',
	        fieldLabel: '',
	        columns: 2,
	        vertical: true,
	        itemId: 'myCheckboxGroup',
	        items: [
	            { boxLabel: '医嘱单号', name: 'rb', checked: false, inputValue: 'OSUserOrderForm_DOFID'},
	            { boxLabel: '消费单号', name: 'rb', checked: false, inputValue: 'OSUserOrderForm_OSUserConsumerFormID'},
	            { boxLabel: '大家价格', name: 'rb', checked: false, inputValue: 'OSUserOrderForm_GreatMasterPrice' },
	            { boxLabel: '折扣价格', name: 'rb',  checked: false,inputValue:'OSUserOrderForm_DiscountPrice' }
	        ]
		}];       
		return items;
	},
	
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		var values = me.getForm().getValues();
		me.saveStore(values);
		me.fireEvent('save',me,values);
	},
	/**更改标题*/
	changeTitle: function() {},
 
     /**本地数据存储*/
	LocalStorage :  {
	    set: function (name, value) {
	        localStorage.setItem(name, value);
	    },
	    get: function (name) {
	        return localStorage.getItem(name);
	    },
	    remove: function (name) {
	        localStorage.removeItem(name);
	    }
	 },
	/**本地保存localstorage*/
	saveStore:function(values){
		var me=this;
   
        if( me.LocalStorage.get('OSUserOrderFormGrid')){
        	me.LocalStorage.remove('OSUserOrderFormGrid');
        }
        me.LocalStorage.set('OSUserOrderFormGrid',JSON.stringify(values.rb));
	}
});