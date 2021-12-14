/**
   @Name：条码打印 -公共部分
   @Author：liangyl
   @version 2019-04-10
 */
layui.extend({
	uxutil: 'ux/util',
	uxtable:'views/pre/demo/modules/uxtable',
	basic: 'views/pre/demo/barcode/basic'
}).use(['uxutil','table','form','laydate','uxtable'],function(){
	var $ = layui.$,
	    element = layui.element,
		form = layui.form,
		uxutil=layui.uxutil,
		laydate=layui.laydate,
		uxtable=layui.uxtable,
		uxform = layui.uxform,
		table = layui.table;
    //获取医嘱单服务
    var GET_LIST_URL =  uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/mode1.js';
    //日期时间选择器
    laydate.render({
       elem: '#txtstartdate'
      ,type: 'datetime'
    });
    laydate.render({
       elem: '#txtenddate'
      ,type: 'datetime'
    });
    form.render();//需要渲染一下;

	//原始医嘱单
	var doctoradvicetable = table.render({
		elem: '#LAY-original-doctor-advice',
		toolbar:true,
		defaultToolbar: ['filter', 'exports'],
		done: function(res, curr, count) {
			if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
				res.code = 0;
				res.data = [];
			}
		},
		cols: [[
		   {type: 'checkbox', fixed: 'left'},
		   {type: 'numbers',title: '行号',fixed: 'left'},
		   {field: '原始单号',title: '原始单号',width: 100,sort: true},
		   {field: '床号',title: '床号',width: 100,sort: true},
		   {field: '姓名',title: '姓名',width: 100,sort: true},
		   {field: '性别',width: 100,	title: '性别',sort: true},
		   {field: '年龄描述',width: 120,title: '年龄描述',sort: true},
		   {field: '病历号',width: 100,title: '病历号',sort: true},
		   {field: '科室',width: 150,title: '科室',sort: true}, 
		   {field: '医嘱项目',width: 200,title: '医嘱项目',sort: true} ,
		   {field: '就诊类型',width: 100,title: '就诊类型',sort: true}, 
		   {field: '医生',width: 100,title: '医生',sort: true} ]
		],
		text: {none: '暂无相关数据'},
		page: false,
		limit: 1000,
		height: 'full-116'
//		height: $(document).height() - $('#LAY-original-doctor-advice').offset().top-25
	});
	 //数据加载
    function onSearch (){
    	var whereStr = getHql();
    	var where ={limit:10000,page:1};
    	uxtable.onSearch(doctoradvicetable,GET_LIST_URL,where);
    }
    //获取查询表单数据
    function getFormValues(){
    	var obj = {};
		var t = $('#LAY-search-form [name]').serializeArray();
		$.each(t, function() {
			obj[this.name] = this.value;
		});
		return obj;
    }
    //查询条件
    function getHql(obj){
    	if(!obj)obj=getFormValues();
//		if(obj.txtstartdate) 
	
		var strWhere = '';
		var hql = strWhere;
       return hql;
    }
	//查询
	form.on('submit(LAY-from-originaladvice-search)', function(data) {
	    var whereStr = getHql(data.field);
    	var where ={limit:10000,page:1};
    	var indexs=layer.load(2);
		setTimeout(function() {
    	    uxtable.onSearch(doctoradvicetable,GET_LIST_URL,where);
    	    layer.close(indexs);
		}, 200);
	});
	onSearch();
});