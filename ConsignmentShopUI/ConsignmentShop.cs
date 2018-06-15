using ConsignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();

        //Create a list for Shopping Cart
        private List<Item> shoppingCartData = new List<Item>();

        // *** This binds the data to the Items List box ***
        BindingSource itemsBinding = new BindingSource();

        // *** This binds the data to the Shopping Cart box ***
        BindingSource cartBinding = new BindingSource();

        //***This binds the Vendors to the Vendors List box***
        BindingSource vendorsBinding = new BindingSource();


        private decimal storeProfit = 0M;

        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();

            //binds store items to ListBox
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsListBox.DataSource = itemsBinding;

            //Display    this property is an Items property
            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "Display";

            // binds Shopping Cart list to Shopping Cart
            cartBinding.DataSource = shoppingCartData;
            shoppingCartListBox.DataSource = cartBinding;

            // Display    This property is a Shopping Cart property
            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";

            // binds Vendors to Vendors List box
            vendorsBinding.DataSource = store.Vendors;
            vendorsListBox.DataSource = vendorsBinding;

            //Display    this property is a Vendors property
            vendorsListBox.DisplayMember = "Display";
            vendorsListBox.ValueMember = "Display";
        }

        private void SetupData()
        {
            // Add Vendor Data
            store.Vendors.Add(new Vendor { FirstName = "Marlon", LastName = "Mitchell" });
            store.Vendors.Add(new Vendor { FirstName = "Jason", LastName = "Mueller" });
            store.Vendors.Add(new Vendor { FirstName = "Chester", LastName = "Lester" });
            store.Vendors.Add(new Vendor { FirstName = "John", LastName = "Griffin"});

            // Add Items Data
            store.Items.Add(new Item { Title = "Moby Dick", Description = "A book about a whale",
                                       Price = 4.67M,
                                       Owner = store.Vendors[0]});
            store.Items.Add(new Item { Title = "Dumb President", Description = "A book about a Trump",
                                       Price = 0.02M, Owner = store.Vendors[2]});
            store.Items.Add(new Item { Title = "Talking Points", Description = "A book about a speech",
                                       Price = 12.45M, Owner = store.Vendors[1]});
            store.Items.Add(new Item { Title = "Great Controversy", Description = @"E G White's account 
                                                of Good and Evil forces",
                                       Price = 13.75M, Owner = store.Vendors[3]});

            // Name of the store
            store.Name = "The Book Emporium";
            headerText.Text = store.Name;
            
        }
        private void ConsignmentShop_Load(object sender, EventArgs e)
        {

        }

        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            // Figure out what is selected from the items list
            // Copy that item to the Shopping Cart
            // Do we remove the item from the items list? - No
            Item selectedItem = (Item)itemsListBox.SelectedItem;

            shoppingCartData.Add(selectedItem);
            cartBinding.ResetBindings(false);//***This refreshes the Add to Cart ***
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            // Mark each item in the cart as sold
            // Clear the cart
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price;
                storeProfit += (1-(decimal)item.Owner.Commission) * item.Price;
            }
            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();//this removes the seleted items in Items box off the the list

            storeProfitValue.Text = string.Format("{0:C}",storeProfit);

            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }

        private void storeProfitLabel_Click(object sender, EventArgs e)
        {

        }

        private void storeProfitValue_Click(object sender, EventArgs e)
        {

        }

        private void vendorsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
