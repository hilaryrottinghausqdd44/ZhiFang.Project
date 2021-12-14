
/**
 * 检验确认人设置
 * @author liangyl
 * @version 2020-05-08
 */
Ext.define('Shell.class.lts.operate.HandlerForm', {
	extend:'Shell.class.lts.operate.basic.Form',
	title:'检验确认人设置',
	//检验小组
    SectionID:1,
    //检验确认人弹出Handler,审核人弹出Checker，保存到内存时用
	OperateType:'Handler',
	//授权操作类型枚举,检验确认人的Name = '检验确认'
	OperateTypeText:'检验确认',
	//授权操作类型枚举，检验确认人的Id = '2'
	OperateTypeID:'2'
});