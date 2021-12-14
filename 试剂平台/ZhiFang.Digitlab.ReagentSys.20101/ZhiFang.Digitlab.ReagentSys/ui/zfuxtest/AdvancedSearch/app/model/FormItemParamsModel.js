//高级表单查询--全部与查询条件配置:表单每个控件属性的model类
Ext.define("ZhiFang.model.FormItemParamsModel",{
	extend:'Ext.data.Model',
    fields:[
    	{name:'Id',type:'string'},//隐藏ID
    	{name:'CName',type:'string'},//显示名称
    	{name:'EName',type:'string'},//交互字段
    	{name:'Type',type:'string'},//类型 
    	{name:'X',type:'int'},//位置X
    	{name:'Y',type:'int'},//位置Y
    	{name:'Width',type:'int'},//组件宽度
    	{name:'LabelWidth',type:'int'},//显示名称宽度
    	{name:'Height',type:'int'},//组件高度
    	
    	{name:'Url',type:'string'},//URL
    	{name:'IsHidden',type:'bool'},//是否隐藏
    	{name:'Function',type:'string'},//事件
        
        //{name:'Relation',type:'string'},//关系
        //{name:'Operator',type:'string'},//运算符
        
        //{name:'IsComboBox',type:'bool'},//是否下拉,需要配置数据源
        {name:'DataUrl',type:'string'}//配置下拉列表数据源
    ]
})