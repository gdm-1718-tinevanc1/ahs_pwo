using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Project 
    {
        public Int64 Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Startdate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Enddate { get; set; }

        
        public string _Title { get; set; }
        public string _Shorttitle { get; set; }
        public string _Subtitle { get; set; }
        public string _Description { get; set; }
        public string _Abstract { get; set; }

        public string _PartnerValidate { get; set; }
        public string _ParticipantValidate { get; set; }
        public string _FinancingformValidate { get; set; }
        public string _LinkValidate { get; set; }
        public string _BudgetValidate { get; set; }

        public List<ProjectParticipant> Participants { get; set; }
        public List<ProjectPublication> Publications { get; set; }
        public List<ProjectFinancingform> Financingforms { get; set; }

        // public List<ProjectSpearhead> Spearheads { get; set; }
        public List<Link> Links { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Partner> Partners { get; set; }
        public List<Media> Mediums { get; set; }
        public List<Task> Tasks { get; set; }


        public List<ProjectProfile> Profiles { get; set; }

        public Nullable<Int32> BudgetId { get; set; }
        public Budget Budget { get; set; }

        public Nullable<Int64> StatusId { get; set; }
        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        [NotMapped]
        public string Title
        {
            get { return _Title == null ? null : JsonConvert.DeserializeObject<string>(_Title); }
            set { _Title = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public string Shorttitle
        {
            get { return _Shorttitle == null ? null : JsonConvert.DeserializeObject<string>(_Shorttitle); }
            set { _Shorttitle = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public string Subtitle
        {
            get { return _Subtitle == null ? null : JsonConvert.DeserializeObject<string>(_Subtitle); }
            set { _Subtitle = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public string Description
        {
            get { return _Description == null ? null : JsonConvert.DeserializeObject<string>(_Description); }
            set { _Description = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public string Abstract
        {
            get { return _Abstract == null ? null : JsonConvert.DeserializeObject<string>(_Abstract); }
            set { _Abstract = JsonConvert.SerializeObject(value); }
        }


        [NotMapped]
        public string PartnerValidate
        {
            get { return _PartnerValidate == null ? null : JsonConvert.DeserializeObject<string>(_PartnerValidate); }
            set { _PartnerValidate = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public string ParticipantValidate
        {
            get { return _ParticipantValidate == null ? null : JsonConvert.DeserializeObject<string>(_ParticipantValidate); }
            set { _ParticipantValidate = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public string FinancingformValidate
        {
            get { return _FinancingformValidate == null ? null : JsonConvert.DeserializeObject<string>(_FinancingformValidate); }
            set { _FinancingformValidate = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public string LinkValidate
        {
            get { return _LinkValidate == null ? null : JsonConvert.DeserializeObject<string>(_LinkValidate); }
            set { _LinkValidate = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public string BudgetValidate
        {
            get { return _BudgetValidate == null ? null : JsonConvert.DeserializeObject<string>(_BudgetValidate); }
            set { _BudgetValidate = JsonConvert.SerializeObject(value); }
        }
    }
}
