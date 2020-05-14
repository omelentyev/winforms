﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Moq;
using WinForms.Common.Tests;
using Xunit;

namespace System.Windows.Forms.Tests
{
    public class ComboBoxTests
    {
        [Fact]
        public void Ctor_Default()
        {
            var control = new SubComboBox();
            Assert.True(control.AllowSelection);
            Assert.Empty(control.AutoCompleteCustomSource);
            Assert.Same(control.AutoCompleteCustomSource, control.AutoCompleteCustomSource);
            Assert.Equal(AutoCompleteMode.None, control.AutoCompleteMode);
            Assert.Equal(AutoCompleteSource.None, control.AutoCompleteSource);
            Assert.Equal(SystemColors.Window, control.BackColor);
            Assert.Null(control.BackgroundImage);
            Assert.Equal(ImageLayout.Tile, control.BackgroundImageLayout);
            Assert.Equal(0, control.Bounds.X);
            Assert.Equal(0, control.Bounds.Y);
            Assert.Equal(121, control.Bounds.Width);
            Assert.True(control.Bounds.Height > 0);
            Assert.True(control.ClientSize.Width > 0);
            Assert.True(control.ClientSize.Height > 0);
            Assert.Equal(0, control.ClientRectangle.X);
            Assert.Equal(0, control.ClientRectangle.Y);
            Assert.True(control.ClientRectangle.Width > 0);
            Assert.True(control.ClientRectangle.Height > 0);
            Assert.Null(control.DataManager);
            Assert.Null(control.DataSource);
            Assert.Equal(Size.Empty, control.DefaultMaximumSize);
            Assert.Equal(Size.Empty, control.DefaultMinimumSize);
            Assert.Equal(Padding.Empty, control.DefaultPadding);
            Assert.Equal(121, control.DefaultSize.Width);
            Assert.True(control.DefaultSize.Height > 0);
            Assert.Empty(control.DisplayMember);
            Assert.Equal(0, control.DisplayRectangle.X);
            Assert.Equal(0, control.DisplayRectangle.Y);
            Assert.True(control.DisplayRectangle.Width > 0);
            Assert.True(control.DisplayRectangle.Height > 0);
            Assert.Equal(DrawMode.Normal, control.DrawMode);
            Assert.Equal(106, control.DropDownHeight);
            Assert.Equal(ComboBoxStyle.DropDown, control.DropDownStyle);
            Assert.Equal(121, control.DropDownWidth);
            Assert.False(control.DroppedDown);
            Assert.Equal(FlatStyle.Standard, control.FlatStyle);
            Assert.False(control.Focused);
            Assert.Null(control.FormatInfo);
            Assert.Empty(control.FormatString);
            Assert.False(control.FormattingEnabled);
            Assert.Same(Control.DefaultFont, control.Font);
            Assert.Equal(SystemColors.WindowText, control.ForeColor);
            Assert.True(control.Height > 0);
            Assert.True(control.IntegralHeight);
            Assert.Equal(Control.DefaultFont.Height + 2, control.ItemHeight);
            Assert.Empty(control.Items);
            Assert.Same(control.Items, control.Items);
            Assert.Equal(Point.Empty, control.Location);
            Assert.Equal(8, control.MaxDropDownItems);
            Assert.Equal(Size.Empty, control.MaximumSize);
            Assert.Equal(0, control.MaxLength);
            Assert.Equal(Size.Empty, control.MinimumSize);
            Assert.Equal(Padding.Empty, control.Padding);
            Assert.Equal(121, control.PreferredSize.Width);
            Assert.True(control.PreferredSize.Height > 0);
            Assert.Equal(RightToLeft.No, control.RightToLeft);
            Assert.Null(control.SelectedValue);
            Assert.Equal(-1, control.SelectedIndex);
            Assert.Null(control.SelectedItem);
            Assert.Empty(control.SelectedText);
            Assert.Equal(121, control.Size.Width);
            Assert.True(control.Size.Height > 0);
            Assert.Empty(control.Text);
            Assert.Equal(0, control.SelectionLength);
            Assert.Equal(0, control.SelectionStart);
            Assert.False(control.Sorted);
            Assert.Empty(control.ValueMember);
            Assert.Equal(121, control.Width);
        }

        public static IEnumerable<object[]> BackColor_Set_TestData()
        {
            yield return new object[] { Color.Empty, SystemColors.Window };
            yield return new object[] { Color.Red, Color.Red };
        }

        [Theory]
        [MemberData(nameof(BackColor_Set_TestData))]
        public void BackColor_Set_GetReturnsExpected(Color value, Color expected)
        {
            var control = new ComboBox
            {
                BackColor = value
            };
            Assert.Equal(expected, control.BackColor);

            // Set same.
            control.BackColor = value;
            Assert.Equal(expected, control.BackColor);
        }

        [Fact]
        public void BackColor_SetWithHandler_CallsBackColorChanged()
        {
            var control = new ComboBox();
            int callCount = 0;
            EventHandler handler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                callCount++;
            };
            control.BackColorChanged += handler;

            // Set different.
            control.BackColor = Color.Red;
            Assert.Equal(Color.Red, control.BackColor);
            Assert.Equal(1, callCount);

            // Set same.
            control.BackColor = Color.Red;
            Assert.Equal(Color.Red, control.BackColor);
            Assert.Equal(1, callCount);

            // Set different.
            control.BackColor = Color.Empty;
            Assert.Equal(SystemColors.Window, control.BackColor);
            Assert.Equal(2, callCount);

            // Remove handler.
            control.BackColorChanged -= handler;
            control.BackColor = Color.Red;
            Assert.Equal(Color.Red, control.BackColor);
            Assert.Equal(2, callCount);
        }

        [Theory]
        [CommonMemberData(nameof(CommonTestHelper.GetImageTheoryData))]
        public void BackgroundImage_Set_GetReturnsExpected(Image value)
        {
            var control = new ComboBox
            {
                BackgroundImage = value
            };
            Assert.Equal(value, control.BackgroundImage);

            // Set same.
            control.BackgroundImage = value;
            Assert.Equal(value, control.BackgroundImage);
        }

        [Fact]
        public void BackgroundImage_SetWithHandler_CallsBackgroundImageChanged()
        {
            var control = new ComboBox();
            int callCount = 0;
            EventHandler handler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                callCount++;
            };
            control.BackgroundImageChanged += handler;

            // Set different.
            var image1 = new Bitmap(10, 10);
            control.BackgroundImage = image1;
            Assert.Same(image1, control.BackgroundImage);
            Assert.Equal(1, callCount);

            // Set same.
            control.BackgroundImage = image1;
            Assert.Same(image1, control.BackgroundImage);
            Assert.Equal(1, callCount);

            // Set different.
            var image2 = new Bitmap(10, 10);
            control.BackgroundImage = image2;
            Assert.Same(image2, control.BackgroundImage);
            Assert.Equal(2, callCount);

            // Set null.
            control.BackgroundImage = null;
            Assert.Null(control.BackgroundImage);
            Assert.Equal(3, callCount);

            // Remove handler.
            control.BackgroundImageChanged -= handler;
            control.BackgroundImage = image1;
            Assert.Same(image1, control.BackgroundImage);
            Assert.Equal(3, callCount);
        }

        [Theory]
        [CommonMemberData(nameof(CommonTestHelper.GetEnumTypeTheoryData), typeof(ImageLayout))]
        public void BackgroundImageLayout_Set_GetReturnsExpected(ImageLayout value)
        {
            var control = new ComboBox
            {
                BackgroundImageLayout = value
            };
            Assert.Equal(value, control.BackgroundImageLayout);

            // Set same.
            control.BackgroundImageLayout = value;
            Assert.Equal(value, control.BackgroundImageLayout);
        }

        [Fact]
        public void BackgroundImageLayout_SetWithHandler_CallsBackgroundImageLayoutChanged()
        {
            var control = new ComboBox();
            int callCount = 0;
            EventHandler handler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                callCount++;
            };
            control.BackgroundImageLayoutChanged += handler;

            // Set different.
            control.BackgroundImageLayout = ImageLayout.Center;
            Assert.Equal(ImageLayout.Center, control.BackgroundImageLayout);
            Assert.Equal(1, callCount);

            // Set same.
            control.BackgroundImageLayout = ImageLayout.Center;
            Assert.Equal(ImageLayout.Center, control.BackgroundImageLayout);
            Assert.Equal(1, callCount);

            // Set different.
            control.BackgroundImageLayout = ImageLayout.Stretch;
            Assert.Equal(ImageLayout.Stretch, control.BackgroundImageLayout);
            Assert.Equal(2, callCount);

            // Remove handler.
            control.BackgroundImageLayoutChanged -= handler;
            control.BackgroundImageLayout = ImageLayout.Center;
            Assert.Equal(ImageLayout.Center, control.BackgroundImageLayout);
            Assert.Equal(2, callCount);
        }

        [Theory]
        [CommonMemberData(nameof(CommonTestHelper.GetEnumTypeTheoryDataInvalid), typeof(ImageLayout))]
        public void BackgroundImageLayout_SetInvalid_ThrowsInvalidEnumArgumentException(ImageLayout value)
        {
            var control = new ComboBox();
            Assert.Throws<InvalidEnumArgumentException>("value", () => control.BackgroundImageLayout = value);
        }

        public static IEnumerable<object[]> DataSource_Set_TestData()
        {
            yield return new object[] { null };
            yield return new object[] { new List<int>() };
            yield return new object[] { Array.Empty<int>() };

            var mockSource = new Mock<IListSource>(MockBehavior.Strict);
            mockSource
                .Setup(s => s.GetList())
                .Returns(new int[] { 1 });
            yield return new object[] { mockSource.Object };
        }

        [Theory]
        [MemberData(nameof(DataSource_Set_TestData))]
        public void DataSource_Set_GetReturnsExpected(object value)
        {
            var control = new SubComboBox
            {
                DataSource = value
            };
            Assert.Same(value, control.DataSource);
            Assert.Empty(control.DisplayMember);
            Assert.Null(control.DataManager);

            // Set same.
            control.DataSource = value;
            Assert.Same(value, control.DataSource);
            Assert.Empty(control.DisplayMember);
            Assert.Null(control.DataManager);
        }

        [Fact]
        public void DataSource_SetWithHandler_CallsDataSourceChanged()
        {
            var control = new ComboBox();
            int dataSourceCallCount = 0;
            int displayMemberCallCount = 0;
            EventHandler dataSourceHandler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                dataSourceCallCount++;
            };
            EventHandler displayMemberHandler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                displayMemberCallCount++;
            };
            control.DataSourceChanged += dataSourceHandler;
            control.DisplayMemberChanged += displayMemberHandler;

            // Set different.
            var dataSource1 = new List<int>();
            control.DataSource = dataSource1;
            Assert.Same(dataSource1, control.DataSource);
            Assert.Equal(0, dataSourceCallCount);
            Assert.Equal(0, displayMemberCallCount);

            // Set same.
            control.DataSource = dataSource1;
            Assert.Same(dataSource1, control.DataSource);
            Assert.Equal(0, dataSourceCallCount);
            Assert.Equal(0, displayMemberCallCount);

            // Set different.
            var dataSource2 = new List<int>();
            control.DataSource = dataSource2;
            Assert.Same(dataSource2, control.DataSource);
            Assert.Equal(0, dataSourceCallCount);
            Assert.Equal(0, displayMemberCallCount);

            // Set null.
            control.DataSource = null;
            Assert.Null(control.DataSource);
            Assert.Equal(0, dataSourceCallCount);
            Assert.Equal(0, displayMemberCallCount);

            // Remove handler.
            control.DataSourceChanged -= dataSourceHandler;
            control.DisplayMemberChanged -= displayMemberHandler;
            control.DataSource = dataSource1;
            Assert.Same(dataSource1, control.DataSource);
            Assert.Equal(0, dataSourceCallCount);
            Assert.Equal(0, displayMemberCallCount);
        }

        [Theory]
        [CommonMemberData(nameof(CommonTestHelper.GetFontTheoryData))]
        public void Font_Set_GetReturnsExpected(Font value)
        {
            var control = new ComboBox
            {
                Font = value
            };
            Assert.Equal(value ?? Control.DefaultFont, control.Font);

            // Set same.
            control.Font = value;
            Assert.Equal(value ?? Control.DefaultFont, control.Font);
        }

        [Fact]
        public void Font_SetWithHandler_CallsFontChanged()
        {
            var control = new ComboBox();
            int callCount = 0;
            EventHandler handler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                callCount++;
            };
            control.FontChanged += handler;

            // Set different.
            Font font1 = new Font("Arial", 8.25f);
            control.Font = font1;
            Assert.Same(font1, control.Font);
            Assert.Equal(1, callCount);

            // Set same.
            control.Font = font1;
            Assert.Same(font1, control.Font);
            Assert.Equal(1, callCount);

            // Set different.
            Font font2 = SystemFonts.DialogFont;
            control.Font = font2;
            Assert.Same(font2, control.Font);
            Assert.Equal(2, callCount);

            // Set null.
            control.Font = null;
            Assert.Same(Control.DefaultFont, control.Font);
            Assert.Equal(3, callCount);

            // Remove handler.
            control.FontChanged -= handler;
            control.Font = font1;
            Assert.Same(font1, control.Font);
            Assert.Equal(3, callCount);
        }

        public static IEnumerable<object[]> ForeColor_Set_TestData()
        {
            yield return new object[] { Color.Empty, SystemColors.WindowText };
            yield return new object[] { Color.Red, Color.Red };
        }

        [Theory]
        [MemberData(nameof(ForeColor_Set_TestData))]
        public void ForeColor_Set_GetReturnsExpected(Color value, Color expected)
        {
            var control = new ComboBox
            {
                ForeColor = value
            };
            Assert.Equal(expected, control.ForeColor);

            // Set same.
            control.ForeColor = value;
            Assert.Equal(expected, control.ForeColor);
        }

        [Fact]
        public void ForeColor_SetWithHandler_CallsForeColorChanged()
        {
            var control = new ComboBox();
            int callCount = 0;
            EventHandler handler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                callCount++;
            };
            control.ForeColorChanged += handler;

            // Set different.
            control.ForeColor = Color.Red;
            Assert.Equal(Color.Red, control.ForeColor);
            Assert.Equal(1, callCount);

            // Set same.
            control.ForeColor = Color.Red;
            Assert.Equal(Color.Red, control.ForeColor);
            Assert.Equal(1, callCount);

            // Set different.
            control.ForeColor = Color.Empty;
            Assert.Equal(SystemColors.WindowText, control.ForeColor);
            Assert.Equal(2, callCount);

            // Remove handler.
            control.ForeColorChanged -= handler;
            control.ForeColor = Color.Red;
            Assert.Equal(Color.Red, control.ForeColor);
            Assert.Equal(2, callCount);
        }

        [Theory]
        [CommonMemberData(nameof(CommonTestHelper.GetPaddingNormalizedTheoryData))]
        public void Padding_Set_GetReturnsExpected(Padding value, Padding expected)
        {
            var control = new ComboBox
            {
                Padding = value
            };
            Assert.Equal(expected, control.Padding);

            // Set same.
            control.Padding = value;
            Assert.Equal(expected, control.Padding);
        }

        [Fact]
        public void Padding_SetWithHandler_CallsPaddingChanged()
        {
            var control = new ComboBox();
            int callCount = 0;
            EventHandler handler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                callCount++;
            };
            control.PaddingChanged += handler;

            // Set different.
            control.Padding = new Padding(1);
            Assert.Equal(new Padding(1), control.Padding);
            Assert.Equal(1, callCount);

            // Set same.
            control.Padding = new Padding(1);
            Assert.Equal(new Padding(1), control.Padding);
            Assert.Equal(1, callCount);

            // Set different.
            control.Padding = new Padding(2);
            Assert.Equal(new Padding(2), control.Padding);
            Assert.Equal(2, callCount);

            // Remove handler.
            control.PaddingChanged -= handler;
            control.Padding = new Padding(1);
            Assert.Equal(new Padding(1), control.Padding);
            Assert.Equal(2, callCount);
        }

        [Theory]
        [CommonMemberData(nameof(CommonTestHelper.GetRightToLeftTheoryData))]
        public void RightToLeft_Set_GetReturnsExpected(RightToLeft value, RightToLeft expected)
        {
            var control = new ComboBox
            {
                RightToLeft = value
            };
            Assert.Equal(expected, control.RightToLeft);

            // Set same.
            control.RightToLeft = value;
            Assert.Equal(expected, control.RightToLeft);
        }

        [Fact]
        public void RightToLeft_SetWithHandler_CallsRightToLeftChanged()
        {
            var control = new ComboBox();
            int callCount = 0;
            EventHandler handler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                callCount++;
            };
            control.RightToLeftChanged += handler;

            // Set different.
            control.RightToLeft = RightToLeft.Yes;
            Assert.Equal(RightToLeft.Yes, control.RightToLeft);
            Assert.Equal(1, callCount);

            // Set same.
            control.RightToLeft = RightToLeft.Yes;
            Assert.Equal(RightToLeft.Yes, control.RightToLeft);
            Assert.Equal(1, callCount);

            // Set different.
            control.RightToLeft = RightToLeft.Inherit;
            Assert.Equal(RightToLeft.No, control.RightToLeft);
            Assert.Equal(2, callCount);

            // Remove handler.
            control.RightToLeftChanged -= handler;
            control.RightToLeft = RightToLeft.Yes;
            Assert.Equal(RightToLeft.Yes, control.RightToLeft);
            Assert.Equal(2, callCount);
        }

        [Theory]
        [CommonMemberData(nameof(CommonTestHelper.GetEnumTypeTheoryDataInvalid), typeof(RightToLeft))]
        public void RightToLeft_SetInvalid_ThrowsInvalidEnumArgumentException(RightToLeft value)
        {
            var control = new ComboBox();
            Assert.Throws<InvalidEnumArgumentException>("value", () => control.RightToLeft = value);
        }

        [Theory]
        [InlineData(-1, "")]
        [InlineData(0, "System.Windows.Forms.Tests.ComboBoxTests+DataClass")]
        [InlineData(1, "System.Windows.Forms.Tests.ComboBoxTests+DataClass")]
        public void SelectedIndex_SetWithoutDisplayMember_GetReturnsExpected(int value, string expectedText)
        {
            var control = new ComboBox();
            control.Items.Add(new DataClass { Value = "Value1" });
            control.Items.Add(new DataClass { Value = "Value2" });

            control.SelectedIndex = value;
            Assert.Equal(value, control.SelectedIndex);
            Assert.Equal(value == -1 ? null : control.Items[control.SelectedIndex], control.SelectedItem);
            Assert.Equal(expectedText, control.Text);

            // Set same.
            control.SelectedIndex = value;
            Assert.Equal(value, control.SelectedIndex);
            Assert.Equal(value == -1 ? null : control.Items[control.SelectedIndex], control.SelectedItem);
            Assert.Equal(expectedText, control.Text);
        }

        [Theory]
        [InlineData(-1, "")]
        [InlineData(0, "Value1")]
        [InlineData(1, "Value2")]
        public void SelectedIndex_SetWithDisplayMember_GetReturnsExpected(int value, string expectedText)
        {
            var control = new ComboBox
            {
                DisplayMember = "Value"
            };
            control.Items.Add(new DataClass { Value = "Value1" });
            control.Items.Add(new DataClass { Value = "Value2" });

            control.SelectedIndex = value;
            Assert.Equal(value, control.SelectedIndex);
            Assert.Equal(value == -1 ? null : control.Items[control.SelectedIndex], control.SelectedItem);
            Assert.Equal(expectedText, control.Text);

            // Set same.
            control.SelectedIndex = value;
            Assert.Equal(value, control.SelectedIndex);
            Assert.Equal(value == -1 ? null : control.Items[control.SelectedIndex], control.SelectedItem);
            Assert.Equal(expectedText, control.Text);
        }

        public static IEnumerable<object[]> FindString_TestData()
        {
            foreach (int startIndex in new int[] { -2, -1, 0, 1 })
            {
                yield return new object[] { new ComboBox(), null, startIndex, -1 };
                yield return new object[] { new ComboBox(), string.Empty, startIndex, -1 };
                yield return new object[] { new ComboBox(), "s", startIndex, -1 };

                var controlWithNoItems = new ComboBox();
                Assert.Empty(controlWithNoItems.Items);
                yield return new object[] { new ComboBox(), null, startIndex, -1 };
                yield return new object[] { new ComboBox(), string.Empty, startIndex, -1 };
                yield return new object[] { new ComboBox(), "s", startIndex, -1 };
            }

            var controlWithItems = new ComboBox
            {
                DisplayMember = "Value"
            };
            controlWithItems.Items.Add(new DataClass { Value = "abc" });
            controlWithItems.Items.Add(new DataClass { Value = "abc" });
            controlWithItems.Items.Add(new DataClass { Value = "ABC" });
            controlWithItems.Items.Add(new DataClass { Value = "def" });
            controlWithItems.Items.Add(new DataClass { Value = "" });
            controlWithItems.Items.Add(new DataClass { Value = null });

            yield return new object[] { controlWithItems, "abc", -1, 0 };
            yield return new object[] { controlWithItems, "abc", 0, 1 };
            yield return new object[] { controlWithItems, "abc", 1, 2 };
            yield return new object[] { controlWithItems, "abc", 2, 0 };
            yield return new object[] { controlWithItems, "abc", 5, 0 };

            yield return new object[] { controlWithItems, "ABC", -1, 0 };
            yield return new object[] { controlWithItems, "ABC", 0, 1 };
            yield return new object[] { controlWithItems, "ABC", 1, 2 };
            yield return new object[] { controlWithItems, "ABC", 2, 0 };
            yield return new object[] { controlWithItems, "ABC", 5, 0 };

            yield return new object[] { controlWithItems, "a", -1, 0 };
            yield return new object[] { controlWithItems, "a", 0, 1 };
            yield return new object[] { controlWithItems, "a", 1, 2 };
            yield return new object[] { controlWithItems, "a", 2, 0 };
            yield return new object[] { controlWithItems, "a", 5, 0 };

            yield return new object[] { controlWithItems, "A", -1, 0 };
            yield return new object[] { controlWithItems, "A", 0, 1 };
            yield return new object[] { controlWithItems, "A", 1, 2 };
            yield return new object[] { controlWithItems, "A", 2, 0 };
            yield return new object[] { controlWithItems, "A", 5, 0 };

            yield return new object[] { controlWithItems, "abcd", -1, -1 };
            yield return new object[] { controlWithItems, "abcd", 0, -1 };
            yield return new object[] { controlWithItems, "abcd", 1, -1 };
            yield return new object[] { controlWithItems, "abcd", 2, -1 };
            yield return new object[] { controlWithItems, "abcd", 5, -1 };

            yield return new object[] { controlWithItems, "def", -1, 3 };
            yield return new object[] { controlWithItems, "def", 0, 3 };
            yield return new object[] { controlWithItems, "def", 1, 3 };
            yield return new object[] { controlWithItems, "def", 2, 3 };
            yield return new object[] { controlWithItems, "def", 5, 3 };

            yield return new object[] { controlWithItems, null, -1, -1 };
            yield return new object[] { controlWithItems, null, 0, -1 };
            yield return new object[] { controlWithItems, null, 1, -1 };
            yield return new object[] { controlWithItems, null, 2, -1 };
            yield return new object[] { controlWithItems, null, 5, -1 };

            yield return new object[] { controlWithItems, string.Empty, -1, 0 };
            yield return new object[] { controlWithItems, string.Empty, 0, 1 };
            yield return new object[] { controlWithItems, string.Empty, 1, 2 };
            yield return new object[] { controlWithItems, string.Empty, 2, 3 };
            yield return new object[] { controlWithItems, string.Empty, 5, 0 };

            yield return new object[] { controlWithItems, "NoSuchItem", -1, -1 };
            yield return new object[] { controlWithItems, "NoSuchItem", 0, -1 };
            yield return new object[] { controlWithItems, "NoSuchItem", 1, -1 };
            yield return new object[] { controlWithItems, "NoSuchItem", 2, -1 };
            yield return new object[] { controlWithItems, "NoSuchItem", 5, -1 };
        }

        [Theory]
        [MemberData(nameof(FindString_TestData))]
        public void FindString_Invoke_ReturnsExpected(ComboBox control, string s, int startIndex, int expected)
        {
            if (startIndex == -1)
            {
                Assert.Equal(expected, control.FindString(s));
            }

            Assert.Equal(expected, control.FindString(s, startIndex));
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(1)]
        [InlineData(2)]
        public void FindString_InvalidStartIndex_ThrowsArgumentOutOfRangeException(int startIndex)
        {
            var control = new ComboBox();
            control.Items.Add("item");
            Assert.Throws<ArgumentOutOfRangeException>("startIndex", () => control.FindString("s", startIndex));
        }

        public static IEnumerable<object[]> FindStringExact_TestData()
        {
            foreach (int startIndex in new int[] { -2, -1, 0, 1 })
            {
                foreach (bool ignoreCase in new bool[] { true, false })
                {
                    yield return new object[] { new ComboBox(), null, startIndex, ignoreCase, -1 };
                    yield return new object[] { new ComboBox(), string.Empty, startIndex, ignoreCase, -1 };
                    yield return new object[] { new ComboBox(), "s", startIndex, ignoreCase, -1 };

                    var controlWithNoItems = new ComboBox();
                    Assert.Empty(controlWithNoItems.Items);
                    yield return new object[] { new ComboBox(), null, startIndex, ignoreCase, -1 };
                    yield return new object[] { new ComboBox(), string.Empty, startIndex, ignoreCase, -1 };
                    yield return new object[] { new ComboBox(), "s", startIndex, ignoreCase, -1 };
                }
            }

            var controlWithItems = new ComboBox
            {
                DisplayMember = "Value"
            };
            controlWithItems.Items.Add(new DataClass { Value = "abc" });
            controlWithItems.Items.Add(new DataClass { Value = "abc" });
            controlWithItems.Items.Add(new DataClass { Value = "ABC" });
            controlWithItems.Items.Add(new DataClass { Value = "def" });
            controlWithItems.Items.Add(new DataClass { Value = "" });
            controlWithItems.Items.Add(new DataClass { Value = null });

            foreach (bool ignoreCase in new bool[] { true, false })
            {
                yield return new object[] { controlWithItems, "abc", -1, ignoreCase, 0 };
                yield return new object[] { controlWithItems, "abc", 0, ignoreCase, 1 };
                yield return new object[] { controlWithItems, "abc", 1, ignoreCase, ignoreCase ? 2 : 0 };
                yield return new object[] { controlWithItems, "abc", 2, ignoreCase, 0 };
                yield return new object[] { controlWithItems, "abc", 5, ignoreCase, 0 };
            }

            yield return new object[] { controlWithItems, "ABC", -1, false, 2 };
            yield return new object[] { controlWithItems, "ABC", 0, false, 2 };
            yield return new object[] { controlWithItems, "ABC", 1, false, 2 };
            yield return new object[] { controlWithItems, "ABC", 2, false, 2 };
            yield return new object[] { controlWithItems, "ABC", 5, false, 2 };

            yield return new object[] { controlWithItems, "ABC", -1, true, 0 };
            yield return new object[] { controlWithItems, "ABC", 0, true, 1 };
            yield return new object[] { controlWithItems, "ABC", 1, true, 2 };
            yield return new object[] { controlWithItems, "ABC", 2, true, 0 };
            yield return new object[] { controlWithItems, "ABC", 5, true, 0 };

            foreach (bool ignoreCase in new bool[] { true, false })
            {
                yield return new object[] { controlWithItems, "a", -1, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "a", 0, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "a", 1, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "a", 2, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "a", 5, ignoreCase, -1 };

                yield return new object[] { controlWithItems, "A", -1, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "A", 0, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "A", 1, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "A", 2, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "A", 5, ignoreCase, -1 };

                yield return new object[] { controlWithItems, "abcd", -1, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "abcd", 0, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "abcd", 1, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "abcd", 2, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "abcd", 5, ignoreCase, -1 };

                yield return new object[] { controlWithItems, "def", -1, ignoreCase, 3 };
                yield return new object[] { controlWithItems, "def", 0, ignoreCase, 3 };
                yield return new object[] { controlWithItems, "def", 1, ignoreCase, 3 };
                yield return new object[] { controlWithItems, "def", 2, ignoreCase, 3 };
                yield return new object[] { controlWithItems, "def", 5, ignoreCase, 3 };

                yield return new object[] { controlWithItems, null, -1, ignoreCase, -1 };
                yield return new object[] { controlWithItems, null, 0, ignoreCase, -1 };
                yield return new object[] { controlWithItems, null, 1, ignoreCase, -1 };
                yield return new object[] { controlWithItems, null, 2, ignoreCase, -1 };
                yield return new object[] { controlWithItems, null, 5, ignoreCase, -1 };

                yield return new object[] { controlWithItems, string.Empty, -1, ignoreCase, 4 };
                yield return new object[] { controlWithItems, string.Empty, 0, ignoreCase, 4 };
                yield return new object[] { controlWithItems, string.Empty, 1, ignoreCase, 4 };
                yield return new object[] { controlWithItems, string.Empty, 2, ignoreCase, 4 };
                yield return new object[] { controlWithItems, string.Empty, 5, ignoreCase, 4 };

                yield return new object[] { controlWithItems, "NoSuchItem", -1, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "NoSuchItem", 0, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "NoSuchItem", 1, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "NoSuchItem", 2, ignoreCase, -1 };
                yield return new object[] { controlWithItems, "NoSuchItem", 5, ignoreCase, -1 };
            }
        }

        [Theory]
        [MemberData(nameof(FindStringExact_TestData))]
        public void FindStringExact_Invoke_ReturnsExpected(ComboBox control, string s, int startIndex, bool ignoreCase, int expected)
        {
            if (ignoreCase)
            {
                if (startIndex == -1)
                {
                    Assert.Equal(expected, control.FindStringExact(s));
                }

                Assert.Equal(expected, control.FindStringExact(s, startIndex));
            }

            Assert.Equal(expected, control.FindStringExact(s, startIndex, ignoreCase));
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(1)]
        [InlineData(2)]
        public void FindStringExact_InvalidStartIndex_ThrowsArgumentOutOfRangeException(int startIndex)
        {
            var control = new ComboBox();
            control.Items.Add("item");
            Assert.Throws<ArgumentOutOfRangeException>("startIndex", () => control.FindStringExact("s", startIndex));
            Assert.Throws<ArgumentOutOfRangeException>("startIndex", () => control.FindStringExact("s", startIndex, ignoreCase: true));
            Assert.Throws<ArgumentOutOfRangeException>("startIndex", () => control.FindStringExact("s", startIndex, ignoreCase: false));
        }

        private SubComboBox CreateControlForCtrlBackspace(string text = "", int cursorRelativeToEnd = 0)
        {
            var tb = new SubComboBox
            {
                Text = text
            };
            tb.Focus();
            tb.SelectionStart = tb.Text.Length + cursorRelativeToEnd;
            tb.SelectionLength = 0;
            return tb;
        }

        private void SendCtrlBackspace(SubComboBox tb)
        {
            var message = new Message();
            tb.ProcessCmdKey(ref message, Keys.Control | Keys.Back);
        }

        [Fact]
        public void CtrlBackspaceTextRemainsEmpty()
        {
            SubComboBox control = CreateControlForCtrlBackspace();
            SendCtrlBackspace(control);
            Assert.Equal("", control.Text);
        }

        [Theory]
        [CommonMemberData(nameof(CommonTestHelper.GetCtrlBackspaceData))]
        public void CtrlBackspaceTextChanged(string value, string expected, int cursorRelativeToEnd)
        {
            SubComboBox control = CreateControlForCtrlBackspace(value, cursorRelativeToEnd);
            SendCtrlBackspace(control);
            Assert.Equal(expected, control.Text);
        }

        [Theory]
        [CommonMemberData(nameof(CommonTestHelper.GetCtrlBackspaceRepeatedData))]
        public void CtrlBackspaceRepeatedTextChanged(string value, string expected, int repeats)
        {
            SubComboBox control = CreateControlForCtrlBackspace(value);
            for (int i = 0; i < repeats; i++)
            {
                SendCtrlBackspace(control);
            }
            Assert.Equal(expected, control.Text);
        }

        [Fact]
        public void CtrlBackspaceDeletesSelection()
        {
            SubComboBox control = CreateControlForCtrlBackspace("123-5-7-9");
            control.SelectionStart = 2;
            control.SelectionLength = 5;
            SendCtrlBackspace(control);
            Assert.Equal("12-9", control.Text);
        }

        private class SubComboBox : ComboBox
        {
            public new bool AllowSelection => base.AllowSelection;

            public new CurrencyManager DataManager => base.DataManager;

            public new Size DefaultMaximumSize => base.DefaultMaximumSize;

            public new Size DefaultMinimumSize => base.DefaultMinimumSize;

            public new Padding DefaultPadding => base.DefaultPadding;

            public new Size DefaultSize => base.DefaultSize;

            public new bool ProcessCmdKey(ref Message msg, Keys keyData) => base.ProcessCmdKey(ref msg, keyData);
        }

        private class DataClass
        {
            public string Value { get; set; }
        }

        [Fact]
        public void GettingComboBoxItemAccessibleObject_Not_ThrowsException()
        {
            var control = new ComboBox();

            var h1 = new HashNotImplementedObject();
            var h2 = new HashNotImplementedObject();
            var h3 = new HashNotImplementedObject();

            control.Items.AddRange(new[] { h1, h2, h3 });

            var comboBoxAccObj = (ComboBox.ComboBoxAccessibleObject)control.AccessibilityObject;

            var exceptionThrown = false;

            try
            {
                var itemAccObj1 = comboBoxAccObj.ItemAccessibleObjects[h1];
                var itemAccObj2 = comboBoxAccObj.ItemAccessibleObjects[h2];
                var itemAccObj3 = comboBoxAccObj.ItemAccessibleObjects[h3];
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.False(exceptionThrown, "Getting accessible object for ComboBox item has thrown an exception.");
        }

        [Fact]
        public void ComboBox_SelectedIndexChanged_Doesnt_force_Handle_creating()
        {
            string[] items = new[] { "Item 1", "Item 2", "Item 3" };

            var comboBox = new ComboBox();
            Assert.False(comboBox.IsHandleCreated);

            comboBox.Items.AddRange(items);
            comboBox.SelectedItem = items[1];
            Assert.False(comboBox.IsHandleCreated);
        }

        public class HashNotImplementedObject
        {
            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }
        }
    }
}
