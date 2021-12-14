//高级表单查询--全部与查询条件:配置基础字典表的数据对象的服务地址
Ext.define("ZhiFang.store.FormItemComboBoxStore",{
	extend:'Ext.data.Store',
	model:'ZhiFang.model.FormItemComboBoxModel',
   //storeId : 'formItemComboBoxStore_id',
   //fields : ['value','text'],
    proxy:{  
    type: 'ajax',
	 url: '../data/GetListDictionaryData.json',
	 reader: {
	 type: 'json',
	 root: 'list'
	 }
     ,autoLoad: true
    }
});