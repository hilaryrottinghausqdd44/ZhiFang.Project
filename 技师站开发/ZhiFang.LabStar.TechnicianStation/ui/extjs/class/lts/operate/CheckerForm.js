
/**
 * 检验审核人设置
 * @author liangyl
 * @version 2020-05-08
 */
Ext.define('Shell.class.lts.operate.CheckerForm', {
	extend:'Shell.class.lts.operate.basic.Form',
	title:'检验审核人设置',
	//检验小组
    SectionID:1,
    //检验确认人弹出Handler,审核人弹出Checker,保存到内存时用
	OperateType:'Checker',
	//授权操作类型枚举,检验审核人的Name = '审核'
	OperateTypeText:'审核',
	//授权操作类型枚举，检验审核人的Name = 'Id'
	OperateTypeID:'1'
});