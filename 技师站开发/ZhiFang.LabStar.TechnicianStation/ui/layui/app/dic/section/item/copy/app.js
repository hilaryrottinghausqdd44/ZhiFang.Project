/**
   @Name：复制小组项目
   @Author：liangyl
   @version 2019-05-30
 */
layui.extend({
	uxutil: 'ux/util',
	uxtable:'ux/table',
	sectionTable:'app/dic/section/item/copy/sectionTable'
}).use(['uxutil','table','sectionTable'],function(){
	var $ = layui.$,
		uxutil=layui.uxutil,
		sectionTable = layui.sectionTable,
		table = layui.table;
	var params = uxutil.params.get(true);
	//自定义变量
	var config={
		//复制到小组,从外面传进来
		toSectionID:params.SECTIONID,
		checkRowData:[],//选中行
		//小组项目复制服务
		addUrl: uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddCopyLBSectionItem'
	};
    //小组列表功能参数配置
    var options={
    	elem:'#sectiontable',
    	id:'sectiontable',
    	title:'检验小组',
    	size: 'sm', //小尺寸的表格
    	height:'full-50',
    	defaultOrderBy: JSON.stringify([{property: 'LBSection_DispOrder',direction: 'ASC'}])
    };
    sectionTable.render(options);
    //保存
    $('#save').on('click',function(){     	
    	onSaveClick();
    });
    //取消
    $('#cancel').on('click',function(){
    	var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	    parent.layer.close(index); //再执行关闭
    });
    //小组列表行 监听行单击事件
	table.on('row(sectiontable)', function(obj){
		config.checkRowData=[];
		config.checkRowData.push(obj.data);
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	});
    //复制小组项目
	function  onSaveClick(){
		var fromSectionID = '';
		if(config.checkRowData.length>0){
			fromSectionID =config.checkRowData[0].LBSection_Id; 
		}
		if(!config.toSectionID){
			layer.msg('复制到的小组未选择,不能复制！',{icon: 5});
			return;
		}
		if(!fromSectionID){
			layer.msg('请选择从那个小组复制项目！',{icon: 5});
			return;
		}
		var url = config.addUrl+'?fromSectionID='+fromSectionID+'&toSectionID='+config.toSectionID;
		var index2=layer.load();
		uxutil.server.ajax({
			url : url
		}, function(data) {
			//隐藏遮罩层
			if (data.success) {
				layer.msg('保存成功！',{icon:6,time:2000});
				parent.layer.closeAll('iframe');
				parent.afterCopyUpdate(data);
			} else {
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}				
		})
	}
});