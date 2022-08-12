﻿
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	public class DataStore
	{
		public static readonly string[] TaskNames = {
			"Write document", 
			"Research", 
			"Google search", 
			"Develop plan",
			"Consulting", 
			"Buget effort", 
			"Master plan", 
			"Management",
			"Project management", 
			"Testing", 
			"Inventarization", 
			"Create slides",
			"Documentation",
			"Travelling", 
			"Stock update", 
			"Literature",
			"Getting sample data",
			"Conference", 
			"Reading",
			"Summarizing",
			"Team event", 
			"Integration",
			"Planning",
			"Copyright check", 
			"Trademark research", 
			"Sales planning",
			"Time management", 
			"Team event",
			"Scrum meeting",
			"Sales pitch",
			"Budgetting",
		};
		public static readonly string[,] SalesTerms = { {"Absorbed Business"," A company that has been merged into another company."},
                                                {"Absorbed costs","The indirect costs associated with manufacturing, for example, insurance or property taxes."},
                                                {"Account balance","The difference between the debit and the credit sides of an account."},
                                                {"Accrual basis","A method of keeping accounts that shows expenses incurred and income  earned for a given fiscal period, even though such expenses and income have not been actually paid or received in cash."},
                                                {"Amortization"," To liquidate on an installment basis; the process of grad­ually paying off a liability over a period of time."},
                                                {"Audiotaping"," The act of recording onto an audiotape."},
                                                {"Back-to-back loan","an arrangement in which two companies in different countries borrow offsetting amounts in each other's currency and each repays it at a specified future date in its domestic currency. Such a loan, often between a company and its foreign subsidiary, eliminates the risk of loss from exchange rate fluctuations."},
                                                {"Back pay","pay that is owed to an employee for work carried out before the current payment period and is either overdue or results from a backdated pay increase.  "},
                                                {"Ballpark"," an informal term for a rough, estimated figure."},
                                                {"Bill of sale","Formal legal document that conveys title to or interest in specific property from the seller to the buyer."},
                                                {"Business  venture"," Taking financial risks in a commercial enterprise"},
                                                {"Seed money"," a usually modest amount of money used to convert an idea into a viable business. Seed money is a form of venture capital."},
                                                {"Settlement","the payment of a debt or charge."},
                                                {"Sole proprietorship","Business legal structure in which one individual owns the business."},
                                                {"Venture funding","the round of funding for a new company that follows seed funding provided by venture capitalists.    "},
                                                {"Viral marketing","the rapid spread of a message about a new product or service in a similar way to the spread of a virus  "},
                                                {"Vulture capitalist","a venture capitalist who structures deals on behalf of an entrepreneur in such a way that the investors benefit rather than the entrepreneur"},
                                                {"Wallet technology","a software package providing digital wallets or purses on the computers of merchants and customers to facilitate payment by digital cash"},
                                                {"Whistleblowing"," speaking out to the media or the public on malpractice, misconduct, corruption, or mismanagement witnessed in an organization"},
                                                {"Withholding tax","the money that an employer pays directly to the U.S. government as a payment of the income tax on the employee "},
                                                {"Patent","a type of copyright granted as a fixed-term monopoly to an inventor by the state to prevent others copying an invention, or improvement  of a product or process"},
                                                {"Point of purchase","the place at which a product is purchased by the customer. The point of sale can be a retail outlet, a display case, or even a  legal business relationship of two or more people who share responsibilities, resources, profits and liabilities."},
                                                {"Price floor","The lowest amount a business owner can charge for a prod­uct or service and still meet all expenses"},
                                                {"Product mix","All of the products in a seller's total product line."},
                                                {"Psychographics","The system of explaining market behavior in terms of attitudes and life styles."},
                                                {"Added value ","the element(s) of service or product that a sales person or selling organization provides, that a customer is prepared to pay for because of the benefit(s) obtained. "},
                                                {"Advantage","the aspect of a product or service that makes it better than another, especially the one in-situ or that of a competitor"},
                                                {"Benefit","the gain (usually a tangible cost, but can be intangible) that accrues to the customer from the product or service."},
                                                {"Buying warmth","behavioural, non-verbal and other signs that a prospect likes what he sees; very positive from the sales person's perspective, but not an invitation to jump straight to the close"},
                                                {"Commodities","typically a term applied to describe products which are mature in development, produced and sold in vast scale, involving little or no uniqueness between variations of different suppliers; high volume, low price, low profit margin, de-skilled ('ease of use' in consumption, application, installation, etc)."},
                                                {"Deal ","common business parlance for the sale or purchase (agreement or arrangement). It is rather a colloquial term so avoid using it in serious company as it can sound flippant and unprofessional."},
                                                {"Deliverable","an aspect of a proposal that the provider commits to do or supply, usually and preferably clearly measurable."},
                                                {"Influencer ","a person in the prospect organization who has the power to influence and persuade a decision-maker."},
                                                {"Intangible ","in a selling context this describes, or is, an aspect of the product or service offering that has a value but is difficult to see or quantify (for instance, peace-of-mind, reliab