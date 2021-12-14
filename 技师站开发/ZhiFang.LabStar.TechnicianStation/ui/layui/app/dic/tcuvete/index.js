layui.extend({
	uxutil:'/ux/util',
	uxtable:'/ux/table',
	tcuveteform:'app/dic/tcuvete/form',
	tcuveteTable:'app/dic/tcuvete/list'
}).use(['tcuveteTable','tcuveteform','table','form'],function(){
	var $ = layui.$,
		element = layui.element,
		table = layui.table,
		form = layui.form,
		tcuveteTable = layui.tcuveteTable,
		tcuveteform = layui.tcuveteform;	
		
	var config ={
		//当前选择行数据
	    checkRowData:[],
	    //保存后返回的行id
	    PK:null
	};
	
    //采样管列表实例
    var table_ind  = null;
    //表单实例
    var form_ind = null;
	//采样管列表功能参数配置
    var options={
    	elem:'#tcuvete-table',
    	title:'采样管',
    	height:'full-70',
    	size:'sm',
    	defaultOrderBy: JSON.stringify([{property: 'LBTcuvete_DispOrder',direction: 'ASC'}]),
    	done: function(res, curr, count) {
			if(count>0){
				//默认选择第一行
				var rowIndex = 0;
	            for (var i = 0; i < res.data.length; i++) {
	                if (res.data[i].LBTcuvete_Id == config.PK) {
	              	    rowIndex=res.data[i].LAY_TABLE_INDEX;
	              	  break;
	                }
	            }
	            //默认选择行	
	            var tableDiv = $("#tcuvete-table+div .layui-table-body.layui-table-body.layui-table-main");
		        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
		        thatrow.click();

			    //采样管颜色（背景）
			    var that = this.elem.next();
	            for(var i=0;i<res.data.length;i++){
	            	if(res.data[i].LBTcuvete_ColorValue){
	                    that.find(".layui-table-box tbody tr[data-index='" + i + "']").find('td:eq(0)').css("background-color", res.data[i].LBTcuvete_ColorValue);
	            	}
	            }
			}else{
				form_ind.isAdd();
			}
		}
    };
	table_ind = tcuveteTable.render(options);

	//列表监听
	table_ind.table.on('row(tcuvete-table)', function(obj){
		config.checkRowData=[];
		config.checkRowData.push(obj.data);
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        
        form_ind.isEdit(config.checkRowData[0].LBTcuvete_Id);
	});
	form_ind = tcuveteform.render();
     //表单保存后处理
	layui.onevent("form", "save", function(obj) {
		var formtype = obj.formtype;
		var msg = '';
		if(formtype=='add')msg='新增成功!';
		   else msg='修改成功!';
		config.PK = obj.id;
		layer.msg(msg,{icon:6,time:2000});
		tcuveteTable.onSearch();
	});
	//删除成功返回处理
	layui.onevent("form", "del", function(obj) {
		tcuveteTable.onSearch();
	});
	//列表查询
    form.on('submit(btnsearch)', function (data) {
    	var hql = '';
    	//模糊查询
    	if(data.field.search){
    		hql="(lbtcuvete.CName like '%" + data.field.search + 
    		"%' or lbtcuvete.SCode like '%" + data.field.search + "%' or lbtcuvete.SName like '%" +data.field.search+"%')";
    	}
    	tcuveteTable.onSearch(hql);
    });
    //回车事件
    $("#search").on('keydown', function (event) {
        if (event.keyCode == 13) {
        	var hql = '';
        	var search = $("#search").val();
	    	//模糊查询
	    	if(search){
	    		hql="(lbtcuvete.CName like '%" + search + 
	    		"%' or lbtcuvete.SCode like '%" + search + "%' or lbtcuvete.SName like '%" +search+"%')";
	    	}
	    	tcuveteTable.onSearch(hql);
            return false;
        }
    });
    $('#edit').on('click',function(){
		if(!config.checkRowData[0].LBTcuvete_Id){
			layer.msg("请重新选择行再做编辑操作！", { icon: 5, anim: 6 });
			return;
		}
 		form_ind.isEdit(config.checkRowData[0].LBTcuvete_Id);
	});
   
	
	//初始化
	function init(){
		//表单高度
	    var height = $(document).height() - $("#LBTcuvete").offset().top-50;
	    $('#LBTcuvete').css("height",height);
	}
	 // 窗体大小改变时，调整高度显示
	$(window).resize(function() {
		var height = $(window).height() - 100;
	    $('#LBTcuvete').css("height",height);
	});
	//初始化
	init();
});
