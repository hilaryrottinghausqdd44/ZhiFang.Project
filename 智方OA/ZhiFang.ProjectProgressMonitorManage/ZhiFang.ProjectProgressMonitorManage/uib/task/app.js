$(function(){
	
	function getTaskListData(callback){
		var url = JShell.System.Path.ROOT + 
			"/BaseService.svc/ST_UDTO_SearchFTaskByHQL?isPlanish=true";
		$.ajax({
			url:url,
			cache:false,
			dataType:'json',
			success:function(data) {
				if(data == true){
					var value = JShell.JSON.decode(data.ResultDataValue) || {};
					callback(value);
				}else{
					callback({msg:data.ErrorInfo});
				}  
			},  
			error : function() {
				callback({msg:'数据异常'});
			}
		});
	}

	function cellStyle(e, t, o) {
		var n = ["active", "success", "info", "warning", "danger"];
		return o % 2 === 0 && o / 2 < n.length ? {
			classes: n[o / 2]
		} : {}
	}
	function rowStyle(e, t) {
		var o = ["active", "success", "info", "warning", "danger"];
		return t % 2 === 0 && t / 2 < o.length ? {
			classes: o[t / 2]
		} : {}
	}
	function scoreSorter(e, t) {
		return e > t ? 1 : t > e ? -1 : 0
	}
	function nameFormatter(e) {
		return e + '<i class="icon wb-book" aria-hidden="true"></i> '
	}
	function starsFormatter(e) {
		return '<i class="icon wb-star" aria-hidden="true"></i> ' + e
	}
	function queryParams() {
		return {
			type: "owner",
			sort: "updated",
			direction: "desc",
			per_page: 100,
			page: 1
		}
	}
	function buildTable(e, t, o) {
		var n, l, s, a = [],
			c = [];
		for (n = 0; t > n; n++) a.push({
			field: "字段" + n,
			title: "单元" + n
		});
		for (n = 0; o > n; n++) {
			for (s = {}, l = 0; t > l; l++) s["字段" + l] = "Row-" + n + "-" + l;
			c.push(s)
		}
		e.bootstrapTable("destroy").bootstrapTable({
			columns: a,
			data: c,
			iconSize: "outline",
			icons: {
				columns: "glyphicon-list"
			}
		})
	}!
	function(e, t, o) {
		"use strict";
		!
		function() {
			var e = [{
				Tid: "1",
				First: "奔波儿灞",
				sex: "男",
				Score: "50"
			}, {
				Tid: "2",
				First: "灞波儿奔",
				sex: "男",
				Score: "94"
			}, {
				Tid: "3",
				First: "作家崔成浩",
				sex: "男",
				Score: "80"
			}, {
				Tid: "4",
				First: "韩寒",
				sex: "男",
				Score: "67"
			}, {
				Tid: "5",
				First: "郭敬明",
				sex: "男",
				Score: "100"
			}, {
				Tid: "6",
				First: "马云",
				sex: "男",
				Score: "77"
			}, {
				Tid: "7",
				First: "范爷",
				sex: "女",
				Score: "87"
			}];
			o("#exampleTableFromData").bootstrapTable({
				data: e,
				height: "250"
			})
		}(), function() {
			o("#exampleTableColumns").bootstrapTable({
				url: "js/demo/bootstrap_table_test.json",
				height: "400",
				iconSize: "outline",
				showColumns: !0,
				icons: {
					refresh: "glyphicon-repeat",
					toggle: "glyphicon-list-alt",
					columns: "glyphicon-list"
				}
			})
		}(), buildTable(o("#exampleTableLargeColumns"), 50, 50), function() {
			o("#exampleTableToolbar").bootstrapTable({
				url: "js/demo/bootstrap_table_test2.json",
				search: !0,
				showRefresh: !0,
				showToggle: !0,
				showColumns: !0,
				toolbar: "#exampleToolbar",
				iconSize: "outline",
				icons: {
					refresh: "glyphicon-repeat",
					toggle: "glyphicon-list-alt",
					columns: "glyphicon-list"
				}
			})
		}(), function() {
			o("#exampleTableEvents").bootstrapTable({
				url: "js/demo/bootstrap_table_test.json",
				search: !0,
				pagination: !0,
				showRefresh: !0,
				showToggle: !0,
				showColumns: !0,
				iconSize: "outline",
				toolbar: "#exampleTableEventsToolbar",
				icons: {
					refresh: "glyphicon-repeat",
					toggle: "glyphicon-list-alt",
					columns: "glyphicon-list"
				}
			});
			var e = o("#examplebtTableEventsResult");
			o("#exampleTableEvents").on("all.bs.table", function(e, t, o) {
				console.log("Event:", t, ", data:", o)
			}).on("click-row.bs.table", function() {
				e.text("Event: click-row.bs.table")
			}).on("dbl-click-row.bs.table", function() {
				e.text("Event: dbl-click-row.bs.table")
			}).on("sort.bs.table", function() {
				e.text("Event: sort.bs.table")
			}).on("check.bs.table", function() {
				e.text("Event: check.bs.table")
			}).on("uncheck.bs.table", function() {
				e.text("Event: uncheck.bs.table")
			}).on("check-all.bs.table", function() {
				e.text("Event: check-all.bs.table")
			}).on("uncheck-all.bs.table", function() {
				e.text("Event: uncheck-all.bs.table")
			}).on("load-success.bs.table", function() {
				e.text("Event: load-success.bs.table")
			}).on("load-error.bs.table", function() {
				e.text("Event: load-error.bs.table")
			}).on("column-switch.bs.table", function() {
				e.text("Event: column-switch.bs.table")
			}).on("page-change.bs.table", function() {
				e.text("Event: page-change.bs.table")
			}).on("search.bs.table", function() {
				e.text("Event: search.bs.table")
			})
		}()
	}(document, window, jQuery);
});