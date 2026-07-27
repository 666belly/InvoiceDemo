# Invoice Demo

A C# WPF desktop application for viewing and managing invoice details with dynamic calculation capabilities. This demo application showcases invoice management with support for company information, line items, tax calculations, and discount processing.

## Overview

Invoice Demo is a professional invoice viewer and calculator built with Windows Presentation Foundation (WPF). It allows users to:
- Load invoice files containing company and itemized details
- View comprehensive invoice information including sender and receiver details
- Calculate total prices with tax and discount support
- Manage multiple line items with individual pricing
- Handle date selection for invoice and due dates
- Display formatted invoice data in an organized interface

## Features

- **Invoice Loading**: Open and load invoice data from text files (.txt)
- **Company Information Display**: View sender and receiver company details
- **Line Item Management**: Support for multiple products/services with:
  - Product descriptions
  - Quantities
  - Unit prices
  - Tax percentages

- **Automatic Calculations**:
  - Line item totals with tax included
  - Aggregate total price calculation
  - Discount percentage application
  - Real-time total updates

- **Date Management**:
  - Invoice date selection with date picker
  - Due date selection with date picker
  - Formatted date display (yyyy-MM-dd)

- **Flexible UI**:
  - Multiple organized list boxes for different data sections
  - Dynamic visibility toggling based on invoice load state
  - Separate sections for invoice details, company info, items, and sender information
  - Currency formatting for price display

## Project Structure

### Core Classes

- **`MainWindow.xaml` / `MainWindow.xaml.cs`** - Main application window featuring:
  - Menu bar for file operations (Open Invoice, Exit)
  - Multiple ListBox controls for organizing invoice data
  - DatePicker controls for invoice and due dates
  - TextBox for discount input
  - Label for total price display
  - Data binding templates for formatted display
  
- **`AddressInfo` Record** - C# record type containing:
  - Name
  - Street address
  - Zip code
  - City
  - Country
  - Phone number
  - Homepage URL
  - Immutable by design using init-only properties

- **`Invoices.cs`** - Invoice management class that:
  - Extends MainWindow
  - Maintains a list of invoice tuples
  - Stores invoice number, date, and due date
  - Provides methods for adding and retrieving invoices

### UI Components

- **Invoice ListBox** - Displays:
  - Invoice number
  - Invoice date (with date picker)
  - Due date (with date picker)

- **Company ListBox** - Shows:
  - Company name
  - Street address
  - Zip code
  - City
  - Country

- **Additional Details ListBox** - Contains multiple line items with:
  - Product/service descriptions
  - Quantities
  - Unit prices
  - Tax percentages

- **Sender ListBox** - Displays billing/sender information using AddressInfo record

- **Discount and Total Section**:
  - Discount percentage input field
  - Real-time total calculation display

## File Format

Invoice data is loaded from plain text files with a specific line-based format:

```
Line 1:  Invoice Number (e.g., 22-1103)
Line 2:  Invoice Date (yyyy-MM-dd)
Line 3:  Due Date (yyyy-MM-dd)
Line 4:  Company Name
Line 5:  Contact Person
Line 6:  Street Address
Line 7:  Zip Code
Line 8:  City
Line 9:  Country
Line 10: (Reserved)
Line 11: Item 1 Description
Line 12: Item 1 Quantity
Line 13: Item 1 Unit Price
Line 14: Item 1 Tax Percentage
Line 15: Item 2 Description
Line 16: Item 2 Quantity
Line 17: Item 2 Unit Price
Line 18: Item 2 Tax Percentage
Line 19: Sender/Billing Company Name
Line 20: Sender Street Address
Line 21: Sender Zip Code
Line 22: Sender City
Line 23: Sender Country
Line 24: Sender Phone Number
Line 25: Sender Homepage URL
```

### Example File

The repository includes `InvoiceDemo1.txt` as a reference invoice file containing:
- Invoice 22-1103
- Dates: March 1, 2022 to April 15, 2022
- Company: Sana Inc.
- Items: Coca Cola and Chocolate with Vanilla
- Sender: Apu Beverages Inc (Sweden)

## Getting Started

### Prerequisites

- .NET Framework or .NET Core
- Visual Studio or Visual Studio Code with C# support
- Windows OS (for WPF support)

### Building and Running

1. Clone the repository
2. Open `VT24A6.sln` in Visual Studio
3. Build the solution (Build > Build Solution)
4. Run the application (F5 or Debug > Start Debugging)

### Usage Guide

1. **Opening an Invoice**:
   - Click File > Open Invoice
   - Select a .txt invoice file
   - Application loads and displays all invoice data

2. **Viewing Invoice Details**:
   - Invoice section shows number, date, and due date
   - Company section displays seller information
   - Additional Details section shows itemized products/services
   - Sender section displays billing entity information

3. **Applying Discounts**:
   - Enter discount percentage in the Discount field
   - Total automatically recalculates with discount applied
   - Discount is calculated as a percentage of the subtotal

4. **Managing Dates**:
   - Use date pickers to modify invoice date
   - Use date pickers to modify due date
   - Dates sync with the ListBox display

5. **Viewing Total**:
   - Total automatically calculates from all line items
   - Includes tax for each item
   - Deducts discount percentage if specified
   - Displays in currency format (C2)

## How Calculations Work

### Line Item Total
```
Item Total = Quantity × Unit Price × (1 + Tax Percentage / 100)
```

### Invoice Total
```
Subtotal = Sum of all Item Totals
Discount Amount = Subtotal × (Discount Percentage / 100)
Final Total = Subtotal - Discount Amount
```

### Real-time Updates
- Text changed event on discount field triggers recalculation
- Date picker changes update display automatically
- All calculations use invariant culture for consistent number parsing

## Technologies Used

- **Language**: C# (.NET)
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Data Structures**: C# Records (AddressInfo), KeyValuePair, Lists, Tuples
- **File I/O**: File.ReadAllLines() for text parsing
- **Data Binding**: XAML data templates for formatted display
- **Number Formatting**: CultureInfo.InvariantCulture for reliable parsing

## Architecture Highlights

- **Immutable Data**: AddressInfo uses init-only properties for immutability
- **Tuple-Based Storage**: Invoices stored as tuples for lightweight data representation
- **Data Templates**: XAML templates provide clean separation of data and presentation
- **Event-Driven**: Real-time calculations triggered by UI events
- **File-Based**: Text file format allows easy data exchange and version control

## Future Enhancements

- Support for multiple line items with dynamic UI
- Invoice generation and export to PDF
- Database integration for persistent invoice storage
- Email delivery of invoices
- Payment status tracking
- Recurring invoice templates
- Multi-currency support
- Invoice history and archival
- Search and filter functionality
- Advanced reporting and analytics

## Sample Data

The repository includes `InvoiceDemo1.txt` as a working example:
- Demonstrates the expected invoice file format
- Contains realistic business data (Sana Inc. selling to Apu Beverages Inc)
- Includes two line items with tax calculations
- Shows international invoice scenario (Japan to Sweden)

## License

This project is provided as-is without a specified license.

## Author

Created by @pixabel

---

*Last Updated: September 2024*
