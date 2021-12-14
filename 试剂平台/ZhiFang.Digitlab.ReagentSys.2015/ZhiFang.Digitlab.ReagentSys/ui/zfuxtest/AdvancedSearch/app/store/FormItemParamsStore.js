Ext.define("ZhiFang.store.FormItemParamsStore",{
	extend:'Ext.data.Store',
	model:'ZhiFang.model.FormItemParamsModel',
    
    proxy:{  
        type:'ajax',
        //url:'data/GetFormItemInfo.json',//服务地址
        reader:{
        	type:'json',
        	root:'list'
        }
    }
});