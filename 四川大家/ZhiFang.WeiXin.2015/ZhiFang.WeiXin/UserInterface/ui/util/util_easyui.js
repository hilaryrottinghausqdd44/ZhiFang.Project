/**
 * 系统公用功能类-easyui功能通用
 * @type 
 */
var Shell = Shell || {};

Shell.easyuiUtil = Shell.easyuiUtil || {};

/**easyui-datagrid辅助方法*/
Shell.easyuiUtil.DataGrid = {
	/**后台ResultDataValue数据格式转化为前台数据格式{tolal,rows}*/
	loadFilter:function(data){
		if(data.success){
			return Shell.util.JSON.decode(data.ResultDataValue);
		}else{
			$.messager.alert("错误信息",data.ErrorInfo,"error");
			return {"tolal":0,"rows":[]};
		}
    }
};

Shell.easyuiUtil.Msg = {
	/**显示错误*/
	showError:function(msg){
		$.messager.alert("错误信息",msg,"error");
	},
	/**弹出信息框*/
	show:function(config){
		var maxWidth = document.body.clientWidth - 10,
			maxHeight = document.body.clientHeight - 10,
			width = config.width || 500,
			height = config.height || 300;
		$.messager.show({
			title:config.title || "",
			msg:config.msg || "",
			timeout:config.timeout || 2000,
			width:(maxWidth > width ? width : maxWidth),
			height:(maxHeight > height ? height : maxHeight),
			style:config.style || {
				right:'',
				top:document.body.scrollTop+document.documentElement.scrollTop,
				bottom:''
			}
		});
	}
};

$.ajaxSetup ({
   cache:false //关闭AJAX相应的缓存
});

//给datagrid组件扩展行提示方法
$.extend($.fn.datagrid.methods,{
	showColumnRowTooltip:function(taget,row){
		var t = $(taget);
		this.initColumnRowTooltip(t,row);
	},
	initColumnRowTooltip:function(t,row,tr){
		var opts = t.datagrid("options"),
			index = t.datagrid('getRowIndex',row);
			
        tr = tr || t.datagrid("getPanel").find("div.datagrid-view div.datagrid-body table tr.datagrid-row[datagrid-row-index=" + index + "]");
        
        if(opts.rowTooltip){
            var onShow = function(e){
                var tt = $(this), text = $.isFunction(opts.rowTooltip) ? opts.rowTooltip.call(tr,index,row) : buildText(row);
                tt.tooltip("update",text);
            };
            tr.each(function(){$(this).tooltip({onShow:onShow});});
        } else {
            tr.children("td[field]").each(function(){
                var td = $(this), field = td.attr("field"), colOpts = t.datagrid("getColumnOption",field);
                if(!colOpts || !colOpts.tooltip){return;}
                var cell = td.find("div.datagrid-cell"), onShow = function(e){
                    var tt = $(this), text = $.isFunction(colOpts.tooltip) ? colOpts.tooltip.call(cell,row[field],index,row) : row[field];
                    tt.tooltip("update", text);
                };
                $(cell).tooltip({onShow:onShow});
            });
        }
        function buildText(row) {
            var fields = t.datagrid("getColumnFields"),
            	content = $("<table></table>").css({padding:"5px"});;
            $.each(fields,function (i,field){
            	var colOpts = t.datagrid("getColumnOption",field);
                if (!colOpts || !colOpts.field || !colOpts.title){return;}
                content.append("<tr><td style='text-align: right; width: 150px;'>" + colOpts.title + ":</td><td style='width: 250px;'>" + row[field] + "</td></tr>");
            });
            return content;
        };
    },
	showCellTip:function(t,data){
		var rows = t.datagrid("getRows"),
			opts = t.datagrid("options");
			
        t.datagrid("getPanel").find("div.datagrid-view div.datagrid-body table tr.datagrid-row").each(function(){
        	var tr = $(this), index = parseInt(tr.attr("datagrid-row-index")), row = rows[index];
        	this.initColumnRowTooltip(t,row,tr);
        });
	}
});
