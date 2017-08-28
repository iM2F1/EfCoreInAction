﻿// Copyright (c) 2017 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Chapter07Listings.EfClasses;

namespace Test.Chapter07Listings.EFCode.Configurations
{
    public static class AttendeeConfig
    {
        public static void Configure
            (this EntityTypeBuilder<Attendee> entity)
        {
            entity.HasOne(p => p.Ticket) //#A
                //If I use the command below it fails because of https://github.com/aspnet/EntityFramework/issues/8137
                .WithOne(p => p.Attendee)
                //.WithOne()
                .HasForeignKey<Attendee>
                    (p => p.TicketId) //#B
                .IsRequired();

            entity.HasOne(p => p.Optional)
                .WithOne(p => p.Attend)
                .HasForeignKey<Attendee>("OptionalTrackId");

            entity.HasOne(p => p.Required) //#C
                .WithOne(p => p.Attend)
                .HasForeignKey<Attendee>(
                    "MyShadowFk") //#D
                .IsRequired(); //#E
        }
        /*******************************************************************
        #A This sets up the one-to-one navigational relationship, Ticket, which has a foreign key defined in the Attendee class
        #B Here I specify the property that is the foreign key. Note how I need to provide the class type, as the foreign key could be in the principal or dependent entity class
        #C This sets up the one-to-one navigational relationship, Required, which does not have a foreign key defined for it
        #D I use the HasForeignKey<T> method that takes a string, because it is a shadow property and can only be referred to via a name. Note that I use my own name.
        #E In this case I use IsRequired to say the foreign key should not be nullable
         * ********************************************************************/
    }
}