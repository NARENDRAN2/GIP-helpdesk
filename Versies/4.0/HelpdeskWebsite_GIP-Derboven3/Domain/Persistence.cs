﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using System.Web;
using System.Web.Security;
using MySql.Data.MySqlClient;
// URL ENCRYPTIE:
using System.Security.Cryptography;
using System.IO;

namespace Domain
{
    class Persistence
    {
        private string _connString;
        private MySqlConnection _conn;
        private MySqlCommand _cmd;

        //connectie maken
        public void ConnectieMaken(string psql)
        {
            _connString = "server=localhost; user id=root; Password= Test123); database=dbhelpdesk;";
            _conn = new MySqlConnection(_connString);
            _cmd = new MySqlCommand(psql, _conn);
        }

        //Connectie voor Stored Procedure
        public void ConnectieMakenSP(string psql)
        {
            _connString = "server=localhost; user id=root; Password= Test123); database=dbhelpdesk;";
            _conn = new MySqlConnection(_connString);
            _cmd = new MySqlCommand(psql, _conn);
            _cmd.Connection = _conn;
            _cmd.CommandText = psql;
            _cmd.CommandType = System.Data.CommandType.StoredProcedure;
        }

        // TICKETTEN
        //=========================================================

        //stel de datum opgelost van het ticket in.
        public void set_dtmOpgelost(int pTicketID)
        {
            ConnectieMakenSP("set_dtmOpgelost");
            _cmd.Parameters.AddWithValue("pTicketID", pTicketID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        //stel de datum opgelost van het ticket in.
        public void insert_melding(int pGebruikerID, string pOmschrijving)
        {
            ConnectieMakenSP("insert_melding");
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _cmd.Parameters.AddWithValue("pOmschrijving", pOmschrijving);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        //stel de datum opgelost van het ticket in.
        public void set_gelezen(int pMeldingID)
        {
            ConnectieMakenSP("set_gelezen");
            _cmd.Parameters.AddWithValue("pMeldingID", pMeldingID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        //Lees alle ticketten in die open zijn
        public List<Ticket> read_tickets()
        {
            List<Ticket> lijst = new List<Ticket>();
            Ticket vraag;
            ConnectieMakenSP("read_tickets");
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                vraag = new Ticket(Convert.ToInt16(dr["id"]), dr["gebruiker"].ToString(), dr["categorie"].ToString(), dr["onderwerp"].ToString(), dr["status"].ToString(), Convert.ToDateTime(dr["datumgemaakt"]), Convert.ToDateTime(dr["datumopgelost"]));
                lijst.Add(vraag);
            }
            return lijst;
            _conn.Close();
        }

        //Lees alle ticketten in die open zijn
        public List<Ticket> read_tickets_admin(int pAdminID)
        {
            List<Ticket> lijst = new List<Ticket>();
            Ticket vraag;
            ConnectieMakenSP("read_tickets_admin");
            _cmd.Parameters.AddWithValue("pAdminID", pAdminID);
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                vraag = new Ticket(Convert.ToInt16(dr["id"]), dr["gebruiker"].ToString(), dr["categorie"].ToString(), dr["onderwerp"].ToString(), dr["status"].ToString(), Convert.ToDateTime(dr["datumgemaakt"]), Convert.ToDateTime(dr["datumopgelost"]));
                lijst.Add(vraag);
            }
            _conn.Close();
            return lijst;
        }

        public void set_admin(int pTicketID, int pAdminID)
        {
            ConnectieMakenSP("set_admin");
            _cmd.Parameters.AddWithValue("pAdminID", pAdminID);
            _cmd.Parameters.AddWithValue("pTicketID", pTicketID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        //Lees de meldingen van 1 gebruiker in
        public List<Melding> read_meldingen_gebruiker(int pGebruikerID)
        {
            List<Melding> lijst = new List<Melding>();
            Melding melding;
            ConnectieMakenSP("read_meldingen_gebruiker");
            _conn.Open();
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                melding = new Melding(Convert.ToInt16(dr["id"]), dr["omschrijving"].ToString(), Convert.ToDateTime(dr["dtmGemaakt"]));
                lijst.Add(melding);
            }
            _conn.Close();
            return lijst;
        }

        //Lees alle ticketten van een gebruiker in.
        public List<Ticket> read_tickets_gebruiker(int pGebruikerID)
        {
            List<Ticket> lijst = new List<Ticket>();
            Ticket vraag;
            ConnectieMakenSP("read_tickets_gebruiker");
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                vraag = new Ticket(Convert.ToInt16(dr["id"]), dr["gebruiker"].ToString(), dr["categorie"].ToString(), dr["onderwerp"].ToString(), dr["status"].ToString(), Convert.ToDateTime(dr["datumgemaakt"]), Convert.ToDateTime(dr["datumopgelost"]));
                lijst.Add(vraag);
            }
            _conn.Close();
            return lijst;
        }

        //insert ticket
        public int insert_ticket(int pGebruikerID, int pCategorieID, string pOnderwerp)
        {
            ConnectieMakenSP("insert_ticket");
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _cmd.Parameters.AddWithValue("pCategorieID", pCategorieID);
            _cmd.Parameters.AddWithValue("pOnderwerp", pOnderwerp);
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            if (dr.Read())
            {
                return Convert.ToInt32(dr[0]);
            }
            else
            {
                return 0;
            }
        }

        //Delete een ticket 
        public void delete_ticket(int pTicketID)
        {
            ConnectieMakenSP("delete_ticket");
            _cmd.Parameters.AddWithValue("pTicketID", pTicketID);
            _conn.Open();
            _cmd.ExecuteReader();
        }

        //update een ticket 
        public void update_ticket(int pTicketID, int pCategorieID, int pStatusID, string pOnderwerp)
        {
            ConnectieMakenSP("update_ticket");
            _cmd.Parameters.AddWithValue("pCategorieID", pCategorieID);
            _cmd.Parameters.AddWithValue("pStatusID", pStatusID);
            _cmd.Parameters.AddWithValue("pOnderwerp", pOnderwerp);
            _cmd.Parameters.AddWithValue("pTicketID", pTicketID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        public void update_ticket_status(int pTicketID, int pCategorieID,string pOnderwerp)
        {
            ConnectieMakenSP("update_ticket_status");
            _cmd.Parameters.AddWithValue("pCategorieID", pCategorieID);
            _cmd.Parameters.AddWithValue("pOnderwerp", pOnderwerp);
            _cmd.Parameters.AddWithValue("pTicketID", pTicketID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

    //Tel de gebruikers per soort (bv, admin: 3, gebruiker: 5 Super User;1)
    public int count_gebruikers_soort(string pSoort)
        {
            ConnectieMakenSP("count_gebruikers_soort");
            _cmd.Parameters.AddWithValue("pSoort", pSoort);
            _conn.Open();
            object o = _cmd.ExecuteScalar();
            int hvl = Convert.ToInt16(o);
            _conn.Close();
            return hvl;
        }

        //Tel de tickets per status (bv, Gesloten: 5, onbekend 3)
        public int count_tickets_status(string pStatus)
        {
            ConnectieMakenSP("count_tickets_status");
            _cmd.Parameters.AddWithValue("pStatus", pStatus);
            _conn.Open();
            object o = _cmd.ExecuteScalar();
            int hvl = Convert.ToInt16(o);
            _conn.Close();
            return hvl;
        }

        //Tel het aantal ticketten voor een gebruiker
        public int count_tickets_status_gebruiker(string pStatus, int pGebruikerID)
        {
            ConnectieMakenSP("aantal_status_gebruiker");
            _cmd.Parameters.AddWithValue("pStatus", pStatus);
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _conn.Open();
            object o = _cmd.ExecuteScalar();
            int hvl = Convert.ToInt16(o);
            _conn.Close();
            return hvl;
        }

        //Tel het aantal ticketten voor een gebruiker
        public int read_gebruikerID_naam(string pNaam)
        {
            ConnectieMakenSP("read_gebruikerID_naam");
            _cmd.Parameters.AddWithValue("pNaam", pNaam);
            _conn.Open();
            object o = _cmd.ExecuteScalar();
            int hvl = Convert.ToInt16(o);
            _conn.Close();
            return hvl;
        }

        //Tel het aantal ticketten voor een gebruiker
        public int read_gebruikerID_ticket(int pTicketID)
        {
            ConnectieMakenSP("read_gebruikerID_ticket");
            _cmd.Parameters.AddWithValue("pTicketID", pTicketID);
            _conn.Open();
            object o = _cmd.ExecuteScalar();
            int hvl = Convert.ToInt16(o);
            _conn.Close();
            return hvl;
        }

        public void update_status_ticket_open(int pTicketID)
        {
            ConnectieMakenSP("update_status_ticket_open");
            _cmd.Parameters.AddWithValue("pTicketID", pTicketID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        //Tel het aantal meldingen voor een gebruiker
        public int count_meldingen_gebruiker(int pGebruikerID)
        {
            ConnectieMakenSP("count_meldingen_gebruiker");
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _conn.Open();
            object o = _cmd.ExecuteScalar();
            int hvl = Convert.ToInt16(o);
            _conn.Close();
            return hvl;
        }

        //Tel de tickets van een genruiker
        public int count_tickets_gebruiker(int pGebruikerID)
        {
            ConnectieMakenSP("count_tickets_gebruiker");
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _conn.Open();
            object o = _cmd.ExecuteScalar();
            int hvl = Convert.ToInt16(o);
            _conn.Close();
            return hvl;
        }

        //Tel alle tickets
        public int count_tickets()
        {
            ConnectieMakenSP("count_tickets");
            _conn.Open();
            object o = _cmd.ExecuteScalar();
            int hvl = Convert.ToInt16(o);
            _conn.Close();
            return hvl;
        }

        // ANTWOORDEN
        //=========================================================

        //Lees de antwoorden van een ticket
        public List<Antwoord> read_antwoorden_ticket(int pTicketID)
        {
            List<Antwoord> lijst = new List<Antwoord>();
            Antwoord antwoord;
            ConnectieMakenSP("read_antwoorden_ticket");
            _cmd.Parameters.AddWithValue("pTicketID", pTicketID);
            _conn.Open();

            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                antwoord = new Antwoord(Convert.ToInt16(dr["id"]), dr["omschrijving"].ToString(), dr["gebruiker"].ToString(), Convert.ToDateTime(dr["datumgemaakt"]));
                lijst.Add(antwoord);
            }
            _conn.Close();
            return lijst;
        }

        //insert antwoord
        public void insert_antwoorden(int pGebruikerID, string pOmschrijving, int pTicketID)
        {
            ConnectieMakenSP("insert_antwoorden");
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _cmd.Parameters.AddWithValue("pOmschrijving", pOmschrijving);
            _cmd.Parameters.AddWithValue("pTicketID", pTicketID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        //delete antwoord
        public void delete_antwoorden(int pAntwoordID)
        {
            ConnectieMakenSP("delete_antwoorden");
            _cmd.Parameters.AddWithValue("pAntwoordID", pAntwoordID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        //update ticket van ticketten  
        public void update_antwoord(int pAntwoordID, string pOmschrijving)
        {
            ConnectieMakenSP("update_antwoorden");
            _cmd.Parameters.AddWithValue("pOmschrijving", pOmschrijving);
            _cmd.Parameters.AddWithValue("pAntwoordID", pAntwoordID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        // STATUS & CATEGORIE
        //=========================================================

        //geef alle statussen weer
        public List<Status> statussen()
        {
            List<Status> lijst = new List<Status>();
            Status status;
            ConnectieMakenSP("read_statussen");
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                status = new Status(Convert.ToInt16(dr[0]), dr[1].ToString());
                lijst.Add(status);
            }
            _conn.Close();
            return lijst;
        }

        //geef alle categories weer
        public List<Categorie> categorieen()
        {
            List<Categorie> lijst = new List<Categorie>();
            Categorie categorie;
            ConnectieMakenSP("read_categorieen");
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                categorie = new Categorie(Convert.ToInt16(dr[0]), dr[1].ToString());
                lijst.Add(categorie);

            }
            _conn.Close();
            return lijst;
        }

        // LOGIN SYSTEM
        //=========================================================

        //LOGIN
        public int login(string pNaam, string pPasswoord)
        {
            ConnectieMakenSP("login");
            _cmd.Parameters.AddWithValue("pNaam", pNaam.ToLower());
            string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(pPasswoord, "SHA1");
            _cmd.Parameters.AddWithValue("pPasswoord", hash);
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            if (dr.Read())
            {
                return Convert.ToInt32(dr["id"]);
            }
            else
            {
                return 0;
            }
        }

        //soort gebruiker ophalen
        public string SoortGebruiker(int pGebruikerID)
        {
            ConnectieMakenSP("SoortGebruiker");
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _conn.Open();
            string groep = "onbekend";
            MySqlDataReader dr = _cmd.ExecuteReader();
            if (dr.Read())
            {
                 groep = dr["omschrijving"].ToString();
            }
            _conn.Close();
            return groep;
        }

        //lees alle gebruikers in
        public List<Gebruiker> read_gebruikers()
        {
            List<Gebruiker> lijst = new List<Gebruiker>();
            Gebruiker gebruiker;
            ConnectieMakenSP("read_gebruikers");
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                gebruiker = new Gebruiker(Convert.ToInt32(dr["id"]),dr["naam"].ToString(), dr["email"].ToString(),dr["groep"].ToString(),Convert.ToBoolean(dr["actief"]),Convert.ToDateTime(dr["dtmGeregistreerd"]));
                lijst.Add(gebruiker);
            }
            _conn.Close();
            return lijst;
        }


        //lees alle gebruikers in
        public Gebruiker read_gebruiker_id( int pGebruikerID)
        {
            Gebruiker gebruiker = new Gebruiker();
            ConnectieMakenSP("read_gebruiker_id");
            _conn.Open();
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                gebruiker = new Gebruiker(Convert.ToInt32(dr["id"]), dr["naam"].ToString(), dr["email"].ToString(), dr["groep"].ToString(), Convert.ToBoolean(dr["actief"]), Convert.ToDateTime(dr["dtmGeregistreerd"]));
            }
            _conn.Close();
            return gebruiker;
        }

        //lees alle admins mee in
        public List<Gebruiker> read_gebruikers_admins()
        {
            List<Gebruiker> lijst = new List<Gebruiker>();
            Gebruiker gebruiker;
            ConnectieMakenSP("read_gebruikers_admin");
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                gebruiker = new Gebruiker(Convert.ToInt32(dr["id"]), dr["naam"].ToString(), dr["email"].ToString(), dr["groep"].ToString(), Convert.ToBoolean(dr["actief"]), Convert.ToDateTime(dr["dtmGeregistreerd"]));
                lijst.Add(gebruiker);
            }
            _conn.Close();
            return lijst;
        }

        public bool AvailableNaam(string pNaam)
        {
            ConnectieMakenSP("AvailableNaam");
            _cmd.Parameters.AddWithValue("pNaam", pNaam.ToLower());
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            if (dr.Read())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool AvailableEmail(string pEmail)
        {
            ConnectieMakenSP("AvailableEmail");
            _cmd.Parameters.AddWithValue("pEmail", pEmail);
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            if (dr.Read())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //register
        public string register(string pNaam, string pEmail, string pPasswoord)
        {
                if (AvailableNaam(pNaam) == true)
                {
                if (AvailableEmail(pEmail) == true)
                {
                    ConnectieMakenSP("register");
                    _cmd.Parameters.AddWithValue("pNaam", pNaam.ToLower());
                    string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(pPasswoord, "SHA1");
                    _cmd.Parameters.AddWithValue("pPasswoord", hash);
                    _cmd.Parameters.AddWithValue("pEmail", pEmail);
                    _conn.Open();
                    int i = _cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return "true";
                    }
                    else
                    {
                        return "Er ging iets mis";
                    }
                }
                else
                {
                    return "Email is al in gebruik.";
                }
                }
                else
                {
                    return "Gebruikersnaam al in gebruik.";
                }
        }

        //reset passwoord
        public string ResetPassword(int pGebruikerID, string pOudPasswoord, string pNieuwPasswoord)
        {
            ConnectieMakenSP("verify_user");
            _cmd.Parameters.AddWithValue("pgebruikerID", pGebruikerID);
            string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(pOudPasswoord, "SHA1");
            _cmd.Parameters.AddWithValue("pPasswoord", hash);
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            if (dr.Read())
            {
                ConnectieMakenSP("update_passwoord");
                hash = FormsAuthentication.HashPasswordForStoringInConfigFile(pNieuwPasswoord, "SHA1");
                _cmd.Parameters.AddWithValue("pPasswoord", hash);
                _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
                _conn.Open();
                int i = _cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return "Je paswoord is sucessvol gewijzigd.";
                }
                else
                {
                    return "Er ging iets mis";
                }

            }
            else
            {
                return "Oud passwoord is incorrect.";
            }
        }

        public bool StandaardPasswoord(int pGebruikerID)
        {
            ConnectieMakenSP("update_passwoord");
            string hash = FormsAuthentication.HashPasswordForStoringInConfigFile("Kaso2640", "SHA1");
            _cmd.Parameters.AddWithValue("pPasswoord", hash);
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _conn.Open();
            int i = _cmd.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // BEHEER GEBRUIKERS
        //=========================================================

        //bewerk een gebruiker (of jezelf)
        // nog een return
        public void update_gebruikers(string pNaam, string pEmail, int pGroepID, bool pActief, int pGebruikerID)
        {
            ConnectieMakenSP("update_gebruikers");
            _cmd.Parameters.AddWithValue("pNaam", pNaam.ToLower());
            _cmd.Parameters.AddWithValue("pEmail", pEmail);
            _cmd.Parameters.AddWithValue("pGroepID", pGroepID);
            _cmd.Parameters.AddWithValue("pActief", Convert.ToInt16(pActief));
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        public void update_profiel(string pNaam, string pEmail, int pGebruikerID)
        {
            ConnectieMakenSP("update_profiel");
            _cmd.Parameters.AddWithValue("pNaam", pNaam.ToLower());
            _cmd.Parameters.AddWithValue("pEmail", pEmail);
            _cmd.Parameters.AddWithValue("pGebruikerID", pGebruikerID);
            _conn.Open();
            _cmd.ExecuteReader();
        }

        // groepen inladen
        public List<Role> groepen()
        {
            List<Role> lijst = new List<Role>();
            Role groep;
            ConnectieMakenSP("read_groepen");
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                groep = new Role(Convert.ToInt16(dr[0]), dr[1].ToString());
                lijst.Add(groep);
            }
            _conn.Close();
            return lijst;
        }

        //delete een gebruiker
        public void delete_gebruiker(int gebruikerID)
        {
            string sql = "SELECT * FROM dbhelpdesk.tblantwoorden where gebruikerID = @id;";
            ConnectieMaken(sql);
            _cmd.Parameters.AddWithValue("@id", gebruikerID);
            _conn.Open();
            MySqlDataReader dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                delete_antwoorden(Convert.ToInt16(dr[0]));
            }
            sql = "SELECT * FROM dbhelpdesk.tbltickets where gebruikerID = @id;";
            ConnectieMaken(sql);
            _cmd.Parameters.AddWithValue("@id", gebruikerID);
            _conn.Open();
            dr = _cmd.ExecuteReader();
            while (dr.Read())
            {
                delete_ticket(Convert.ToInt16(dr[0]));
            }
            sql = "DELETE FROM dbhelpdesk.tblgebruikers WHERE id=@gebruikerID;";
            ConnectieMaken(sql);
            _cmd.Parameters.AddWithValue("@gebruikerID", gebruikerID);
            _conn.Open();
            _cmd.ExecuteReader();
            _conn.Close();
        }

        public string Encrypt_QueryString(string str)
        {
            string EncrptKey = "Kaso;2640!)";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt_QueryString(string str)
        {
            str = str.Replace(" ", "+");
            string DecryptKey = "Kaso;2640!)";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[str.Length];

            byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
    }
}
