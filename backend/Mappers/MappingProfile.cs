using AutoMapper;
using backend.DTOs.Products;
using backend.DTOs.Products.Variants;
using backend.DTOs.Orders;
using backend.Models.Products;
using backend.Models.Orders;
using backend.DTOs.Users;
using backend.Models.Users;
using backend.DTOs.Auth;

namespace backend.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // PRODUCTS
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // PRODUCT VARIANTS
            CreateMap<ProductVariant, ProductVariantDto>();
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<CreateProductVariantDto, ProductVariant>()
                .ForMember(dest => dest.Id , opt => opt.Ignore())
                .ForMember(dest => dest.Product , opt => opt.Ignore())
                .ForMember(dest => dest.Color , opt => opt.Ignore())
                .ForMember(dest => dest.Size , opt => opt.Ignore());
            CreateMap<UpdateProductVariantDto, ProductVariant>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Color, opt => opt.Ignore())
                .ForMember(dest => dest.Size, opt => opt.Ignore());            
            // ORDERS
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<OrderDetail, OrderDetailDto>();

            // USERS
            CreateMap<User, UserDto>();
            CreateMap<CreateStaffDto, User>();
        }
    }
}