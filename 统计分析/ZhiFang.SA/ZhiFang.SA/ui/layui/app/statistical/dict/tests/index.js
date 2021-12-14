layui.use(['layer', 'form'], function() {
	var $ = layui.$;
	var form = layui.form;
	
	function openEquip() {
		//刚进来清空缓存 预防存在原来缓存 
		layui.data('equiptabData', null);
		layer.open({
			type: 2,
			title: '选择仪器',
			//area:'500px',
			area: ['78%', '98%'],
			content: '../equipment/dbtable.html',
			end: function() {
				var sumit = layui.data('equiptabData').sumit;
				if (sumit == 0) {
					//仪器点了保存按钮
					//读取缓存
					var data = layui.data('equiptabData').data;
					if(data&&data.length>0)data=JSON.stringify(data);
					$('#equipment_list').text(data);
					//$('#equipment_list').data('data', JSON.stringify(data));
				} else {
					//仪器没有保存关闭窗口
					var data = $('#equipment_list').data('data');
				}
			}
		});
	};

	function openPGroup() {
		layui.data('pgrouptabData', null);
		layer.open({
			type: 2,
			title: '选择小组',
			//area:'500px',
			area: ['78%', '98%'],
			content: '../pgroup/dbtable.html',
			end: function() {
				var sumit = layui.data('pgrouptabData').sumit;
				if (sumit == 0) {
					//仪器点了保存按钮
					//读取缓存
					var data = layui.data('pgrouptabData').data;
					if(data&&data.length>0)data=JSON.stringify(data);
					$('#pgroup_list').text(data);
					//$('#pgroup_list').data('data', JSON.stringify(data));
				} else {
					//仪器没有保存关闭窗口
					var data = $('#pgroup_list').data('data');
				}
			}
		});
	};

	//仪器列表选择
	$('#LAY-tests-btn-equipment').on('click', function() {
		openEquip();
	});
	$('#LAY-tests-btn-equipment2').on('click', function() {
		openEquip();
	});
	//小组列表选择
	$('#LAY-tests-btn-pgroup').on('click', function() {
		openPGroup();
	});
	$('#LAY-tests-btn-pgroup2').on('click', function() {
		openPGroup();
	});
});
