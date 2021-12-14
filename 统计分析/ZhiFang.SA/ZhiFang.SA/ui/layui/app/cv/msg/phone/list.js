layui.config({
	base:'../../../admin/layuiadmin/' //静态资源所在路径
}).extend({
	uxutil: '../../ux/util'
}).use(['table','uxutil'], function(){
	var $ = layui.$,
		table = layui.table,
		uxutil = layui.uxutil;
		
	//获取列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgTypeByHQL';
	var FIELDS = [''];
	
	table.render({
		elem: '#table',
		url: GET_LIST_URL,
		toolbar: '#table-toolbar-top',
		title: '用户数据表',
		cols: [[
			{field:'CName', width:180, title: '消息类型名称', sort: true},
			{field:'Code', width:160, title: '消息类型代码', sort: true},  
			{field:'SystemCName', width:110, title: '所属系统名称', sort: true},
			{field:'SystemCode', width:100, title: '所属系统代码', sort: true},
			{field:'Url', width:200, title: '展现程序地址', sort: true},
			{field:'Visible', width:110, title: '是否可用', sort: true},
			{field:'Memo', width:150, title: '备注', sort: true},
			{width:80,align:'center',fixed:'right',toolbar:'#table-operate-bar'}
		]],
		loading:true,
		page: true,
		parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
			var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": data.list || []
			};
		}
	});
	
	//头工具栏事件
    table.on('toolbar(table)', function(obj){
      var checkStatus = table.checkStatus(obj.config.id);
      switch(obj.event){
      	case 'add':
          layer.alert('新增');
        break;
        case 'getCheckData':
          var data = checkStatus.data;
          layer.alert(JSON.stringify(data));
        break;
        case 'getCheckLength':
          var data = checkStatus.data;
          layer.msg('选中了：'+ data.length + ' 个');
        break;
        case 'isAll':
          layer.msg(checkStatus.isAll ? '全选': '未全选');
        break;
      };
    });
});