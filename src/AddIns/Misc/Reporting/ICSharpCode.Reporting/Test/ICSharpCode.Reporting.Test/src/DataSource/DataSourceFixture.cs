﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.ComponentModel;
using ICSharpCode.Reporting.BaseClasses;
using ICSharpCode.Reporting.DataManager.Listhandling;
using ICSharpCode.Reporting.Interfaces;
using ICSharpCode.Reporting.Items;
using NUnit.Framework;

namespace ICSharpCode.Reporting.Test.DataSource
{
	[TestFixture]
	public class CollectionDataSourceFixture
	{
		 ContributorCollection list;
		
		[Test]
		public void CollectionCountIsEqualToListCount() {
			var collectionSource = new CollectionDataSource	(list,new ReportSettings());
			Assert.That(collectionSource.Count,Is.EqualTo(list.Count));
		}
		
		
		[Test]
		public void AvailableFieldsEqualContibutorsPropertyCount() {
			var collectionSource = new CollectionDataSource	(list,new ReportSettings());
			Assert.That(collectionSource.AvailableFields.Count,Is.EqualTo(6));
		}
		
		
		
		[Test]
		public void GroupbyOneColumn () {
			var rs = new ReportSettings();
			rs.GroupColumnCollection.Add( new GroupColumn("GroupItem",1,ListSortDirection.Ascending));
			var collectionSource = new CollectionDataSource	(list,rs);
			collectionSource.Bind();
		}
		
		
		[Test]
		public void TypeOfReportItemIsString () {
			var ric = new System.Collections.Generic.List<IPrintableObject>(){
				new BaseDataItem(){
					ColumnName = "Lastname"
						
				},
				new BaseDataItem(){
					ColumnName = "Firstname"
				}
			};
			var collectionSource = new CollectionDataSource	(list,new ReportSettings());
			collectionSource.Bind();
			collectionSource.Fill(ric);
			foreach (BaseDataItem element in ric) {
				Assert.That(element.DataType,Is.EqualTo("System.String"));
			}
		}
		
		
		[Test]
		public void SortOneColumnAscending() {
			var ric = new System.Collections.Generic.List<IPrintableObject>(){
				new BaseDataItem(){
					ColumnName = "Lastname"
						
				},
				new BaseDataItem(){
					ColumnName = "Firstname"
				}
				
				
			};
			
			var rs = new ReportSettings();
			rs.SortColumnsCollection.Add(new SortColumn("Lastname",ListSortDirection.Ascending));
			var collectionSource = new CollectionDataSource	(list,rs);
			collectionSource.Bind();
			string compare = String.Empty;
			int i = 0;
			do {
				collectionSource.Fill(ric);
				Console.WriteLine("first : <{0}> Last <{1}> ",((BaseDataItem)ric[0]).DBValue,
				                  ((BaseDataItem)ric[1]).DBValue);
				Assert.That(((BaseDataItem)ric[0]).DBValue,Is.GreaterThanOrEqualTo(compare));
				compare = ((BaseDataItem)ric[0]).DBValue;
				                 
				i ++;
			}while (collectionSource.MoveNext());
			
			Assert.That(i,Is.EqualTo(collectionSource.Count));
		}
		
		
		[Test]
		public void GroupbyOneColumnAndFill () {
			
			var ric = new System.Collections.Generic.List<IPrintableObject>(){
				new BaseDataItem(){
					ColumnName = "Lastname"
						
				},
				new BaseDataItem(){
					ColumnName = "Firstname"
				},
				
				new BaseDataItem(){
					ColumnName = "GroupItem"
				},
				
				new BaseDataItem(){
					ColumnName = "RandomInt"
				}
			};
			var rs = new ReportSettings();
			rs.GroupColumnCollection.Add( new GroupColumn("GroupItem",1,ListSortDirection.Ascending));
			rs.GroupColumnCollection.Add( new GroupColumn("RandomInt",1,ListSortDirection.Ascending));
			
			var collectionSource = new CollectionDataSource (list,rs);
			collectionSource.Bind();
			int i = 0;
			do {
				collectionSource.Fill(ric);
				Console.WriteLine("first : <{0}> Last <{1}> Group <{2}> Randomint <{3}>",((BaseDataItem)ric[0]).DBValue,
				                  ((BaseDataItem)ric[1]).DBValue,
				                  ((BaseDataItem)ric[2]).DBValue,
				                  ((BaseDataItem)ric[3]).DBValue);
				i ++;
			}while (collectionSource.MoveNext());
			
			Assert.That(i,Is.EqualTo(collectionSource.Count));
		}
	
		
		[SetUp]
		public void CreateList() {
			var contributorList = new ContributorsList();
			list = contributorList.ContributorCollection;
		}	
	}
}
